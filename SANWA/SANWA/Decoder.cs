using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANWA.Utility
{
    public class Decoder
    {
        private string Supplier;

        public Decoder(string supplier)
        {
            Supplier = supplier.ToUpper();
        }

        public List<ReturnMessage> GetMessage(string Message)
        {
            List<ReturnMessage> result = null;

            try
            {
                switch (Supplier)
                {
                    case "SANWA":
                    case "ATEL_NEW":
                        result = SANWACodeAnalysis(Message);
                        break;

                    case "TDK":
                        result = TDKCodeAnalysis(Message);
                        break;

                    case "KAWASAKI":
                        result = KAWASAKICodeAnalysis(Message);
                        break;

                    case "HST":
                        result = HSTCodeAnalysis(Message);
                        break;

                    case "COGNEX":
                        result = COGNEXCodeAnalysis(Message);
                        break;

                    case "ATEL":
                        result = ATELCodeAnalysis(Message);
                        break;
                    case "ASYST":
                        result = ASYSTCodeAnalysis(Message);
                        break;
                    case "SMARTTAG":
                        result = SmartTagCodeAnalysis(Message);

                        break;
                    default:
                        throw new NotImplementedException();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        private List<ReturnMessage> SmartTagCodeAnalysis(string Message)
        {
            List<ReturnMessage> result;
           

            try
            {
                result = new List<ReturnMessage>();
                ReturnMessage r = new ReturnMessage();
                r.NodeAdr = "00";
                if (Message.IndexOf("CA") != -1)
                {
                   
                    r.Type = ReturnMessage.ReturnType.Excuted;
                    result.Add(r);
                   
                }
                else if (Message.IndexOf("60") != -1)
                {
                    if (Message.Length > 2)
                    {
                        r.Type = ReturnMessage.ReturnType.Information;
                        r.FinCommand = "9F FF";
                        r.Value = parseTag(Message);
                        r.Value = r.Value.Substring(0, Message.Length - 16);
                    }
                    else
                    {
                        r.Type = ReturnMessage.ReturnType.Excuted;
                        
                      
                    }
                    result.Add(r);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        private string parseTag(string tag)
        {
            string[] datas = tag.Replace("60 AF 0A ", "").Split(' ');
            StringBuilder result = new StringBuilder();
            int lenResult = 0;
            foreach (string data in datas)
            {
                if (data.Equals(""))
                    continue;
                lenResult++;
                if (lenResult > 240)
                    break;//超出資料範圍
                try
                {
                    string chr1 = getReadMappingChar(data.Substring(0, 1));
                    string chr2 = getReadMappingChar(data.Substring(1, 1));
                    string temp = System.Convert.ToChar(System.Convert.ToUInt32(chr2 + chr1, 16)).ToString();
                    if (!chr2.Equals("0") || !chr1.Equals("0"))
                        result.Append(temp + "");//trim null
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            return result.ToString();
        }

        private string getReadMappingChar(string tag)
        {
            Dictionary<string, string> charMap = new Dictionary<string, string>();
            charMap.Add("0", "0");
            charMap.Add("1", "8");
            charMap.Add("2", "4");
            charMap.Add("3", "C");
            charMap.Add("4", "2");
            charMap.Add("5", "A");
            charMap.Add("6", "6");
            charMap.Add("7", "E");
            charMap.Add("8", "1");
            charMap.Add("9", "9");
            charMap.Add("A", "5");
            charMap.Add("B", "D");
            charMap.Add("C", "3");
            charMap.Add("D", "B");
            charMap.Add("E", "7");
            charMap.Add("F", "F");
            return charMap[tag];
        }

        private string CalculateReadChecksum(string dataToCalculate, string checkString)
        {
            byte[] byteToCalculate = Encoding.ASCII.GetBytes(dataToCalculate);
            //byte[] byteToCheck = HexStringToByteArray(checkString);

            int checkSum = 0;
            byte[] bdata = { 0x50 };
            //基底 50
            foreach (byte b in bdata)
            {
                checkSum += b;
            }
            //ASCII 資料 加總
            foreach (byte chData in byteToCalculate)
            {
                checkSum += chData;
            }

            // check sum 附加碼轉 ASCII
            string check1 = checkString.Replace(" ", "").Substring(0, 4);
            string chkChar1 = getReadMappingChar(check1.Substring(0, 1));
            string chkChar2 = getReadMappingChar(check1.Substring(1, 1));
            string chkChar3 = getReadMappingChar(check1.Substring(2, 1));
            string chkChar4 = getReadMappingChar(check1.Substring(3, 1));
            int checkTemp = Int32.Parse(chkChar2 + chkChar1 + chkChar4 + chkChar3, System.Globalization.NumberStyles.HexNumber);
            checkSum = checkSum + checkTemp;

            // Check sum 加密
            string temp = checkSum.ToString("X4");
            string charEncode1 = getReadMappingChar(temp.Substring(0, 1));
            string charEncode2 = getReadMappingChar(temp.Substring(1, 1));
            string charEncode3 = getReadMappingChar(temp.Substring(2, 1));
            string charEncode4 = getReadMappingChar(temp.Substring(3, 1));

            string result = checkString + " " + charEncode4 + charEncode3 + " " + charEncode2 + charEncode1 + " ";
            return result;
        }

        private List<ReturnMessage> COGNEXCodeAnalysis(string Message)
        {
            List<ReturnMessage> result;
            string[] msgAry;

            try
            {
                result = new List<ReturnMessage>();
                msgAry = Message.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach (string Msg in msgAry)
                {
                    if (Msg.Trim().Equals(""))
                    {
                        continue;
                    }
                    ReturnMessage each = new ReturnMessage();
                    each.NodeAdr = "1";
                    each.Command = "";
                    each.OrgMsg = Msg;
                    switch (Msg)
                    {

                        default:
                            each.Type = ReturnMessage.ReturnType.Finished;
                            each.Value = Msg;
                            break;
                    }
                    result.Add(each);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        private List<ReturnMessage> HSTCodeAnalysis(string Msg)
        {
            List<ReturnMessage> result;
            string[] msgAry;

            try
            {
                result = new List<ReturnMessage>();
                //msgAry = Message.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                //foreach (string Msg in msgAry)
                //{
                //if (Msg.Trim().Equals(""))
                //{
                //    continue;
                //}
                ReturnMessage each = new ReturnMessage();
                each.NodeAdr = "1";
                each.Command = "";
                each.OrgMsg = Msg;
                switch (Msg)
                {
                    case "1\r\n":
                        each.Type = ReturnMessage.ReturnType.Excuted;
                        break;
                    case "-2\r\n":
                        each.Type = ReturnMessage.ReturnType.Error;
                        break;
                    default:
                        each.Type = ReturnMessage.ReturnType.Finished;
                        each.Value = Msg.Replace("\r\n", "");
                        break;
                }
                result.Add(each);
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        private List<ReturnMessage> SANWACodeAnalysis(string Message)
        {
            List<ReturnMessage> result;
            string[] msgAry;

            try
            {
                result = new List<ReturnMessage>();
                msgAry = Message.Split('\r');

                foreach (string Msg in msgAry)
                {
                    if (Msg.Trim().Equals(""))
                    {
                        continue;
                    }
                    ReturnMessage each = new ReturnMessage();
                    each.OrgMsg = Msg;
                    each.NodeAdr = Msg[1].ToString();
                    string[] content = Msg.Replace("\r", "").Replace("\n", "").Substring(2).Split(':');
                    for (int i = 0; i < content.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                switch (content[i])
                                {
                                    case "ACK":
                                        each.Type = ReturnMessage.ReturnType.Excuted;
                                        break;
                                    case "NAK":
                                        each.Type = ReturnMessage.ReturnType.Error;
                                        break;
                                    case "FIN":
                                        each.Type = ReturnMessage.ReturnType.Finished;
                                        break;
                                    case "EVT":
                                        each.Type = ReturnMessage.ReturnType.Event;
                                        break;
                                    default:
                                        each.CommandType = content[i];
                                        break;
                                }

                                break;
                            case 1:

                                each.Command = content[i];
                                if (each.Command.Equals("PAUSE") || each.Command.Equals("STOP_"))
                                {
                                    each.IsInterrupt = true;
                                }
                                break;
                            case 2:
                                each.Value = content[i];
                                if (each.Type == ReturnMessage.ReturnType.Finished && !each.Value.Equals("00000000"))
                                {
                                    each.Type = ReturnMessage.ReturnType.Error;
                                }
                                break;
                        }
                    }
                    result.Add(each);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        private List<ReturnMessage> KAWASAKICodeAnalysis(string Message)
        {
            List<ReturnMessage> result;
            string[] msgAry;

            try
            {
                result = new List<ReturnMessage>();
                msgAry = Message.Replace("\r\n", "\r").Split('\r');

                foreach (string Msg in msgAry)
                {
                    if (Msg.Trim().Equals(""))
                    {
                        continue;
                    }
                    ReturnMessage each = new ReturnMessage();
                    each.OrgMsg = Msg;
                    each.Command = Msg.Substring(Msg.IndexOf('<') + 1, Msg.IndexOf('>') - Msg.IndexOf('<') - 1);
                    string[] content = each.Command.Split(',');
                    for (int i = 0; i < content.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                each.Seq = content[0];
                                break;
                            case 1:

                                switch (content[i])
                                {
                                    case "Ack":
                                        each.Type = ReturnMessage.ReturnType.Excuted;
                                        break;
                                    case "Nak":
                                        each.Type = ReturnMessage.ReturnType.Error;
                                        if (content.Length > 2)
                                            each.Value = content[2];
                                        break;
                                    case "Success":
                                        each.Type = ReturnMessage.ReturnType.Finished;
                                        if (content.Length > 2)
                                        {
                                            each.Value = content[2];
                                        }
                                        break;
                                    case "Error":
                                        each.Type = ReturnMessage.ReturnType.Error;
                                        if (content.Length > 3)
                                            each.NodeAdr = content[3].ToString();
                                        if (content.Length > 4)
                                            each.Value = content[2] + ":" + content[4];
                                        break;
                                    default:
                                        each.CommandType = content[1];
                                        break;
                                }
                                break;
                        }
                    }
                    result.Add(each);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        private List<ReturnMessage> TDKCodeAnalysis(string Msg)
        {
            List<ReturnMessage> result;

            try
            {
                result = new List<ReturnMessage>();

                if (Msg.Trim().Equals(""))
                {
                    return result;
                }

                ReturnMessage each = new ReturnMessage();
                byte[] t = new byte[Encoding.ASCII.GetByteCount(Msg.ToString())]; ;
                int c = Encoding.ASCII.GetBytes(Msg.ToString(), 0, Encoding.ASCII.GetByteCount(Msg.ToString()), t, 0);

                each.OrgMsg = Msg;
                each.NodeAdr = Encoding.Default.GetString(t, 3, 2);
                string contentStr = Encoding.Default.GetString(t, 5, t.Length - 5 - 3).Replace(";", "").Trim();

                string[] content = contentStr.Split(':', '/');

                for (int i = 0; i < content.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            switch (content[i])
                            {
                                case "ACK":
                                    each.Type = ReturnMessage.ReturnType.Excuted;
                                    break;
                                case "NAK":
                                    each.Type = ReturnMessage.ReturnType.Error;
                                    break;
                                case "INF":
                                case "RIF":
                                    each.Type = ReturnMessage.ReturnType.Information;
                                    break;
                                case "EVT":
                                    each.Type = ReturnMessage.ReturnType.Event;
                                    break;
                                case "ABS":
                                case "RAS":
                                    each.Type = ReturnMessage.ReturnType.Error;
                                    break;
                                    //case "RIF":
                                    //    each.Type = ReturnMessage.ReturnType.ReInformation;
                                    //    break;
                            }
                            each.CommandType = content[i];
                            break;
                        case 1:

                            each.Command = content[i];
                            if (each.Type == ReturnMessage.ReturnType.Information || each.Type == ReturnMessage.ReturnType.ReInformation || each.Type == ReturnMessage.ReturnType.Error)
                            {
                                each.FinCommand = TDKFinCommand(each.Command);
                            }
                            if (each.Command.Equals("PAUSE") || each.Command.Equals("STOP_"))
                            {
                                each.IsInterrupt = true;
                            }
                            break;
                        case 2:
                            each.Value = content[i];
                            break;
                    }
                }

                result.Add(each);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        private List<ReturnMessage> ATELCodeAnalysis(string Message)
        {
            List<ReturnMessage> result;
            string strMsg = string.Empty;
            string[] msgAry;
            ReturnMessage each;

            try
            {
                result = new List<ReturnMessage>();

                strMsg = Message.Replace("\r", "").Replace("\n", "").Trim();

                if (strMsg.Equals(">") || strMsg.Equals(">*"))
                {
                    each = new ReturnMessage();
                    each.OrgMsg = strMsg;
                    each.NodeAdr = "1";
                    each.Type = ReturnMessage.ReturnType.Excuted;
                    result.Add(each);
                }
                else if (strMsg.Length > 1)
                {
                    msgAry = Message.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    foreach (string Msg in msgAry)
                    {
                        if (Msg.Trim().Equals(""))
                        {
                            continue;
                        }

                        each = new ReturnMessage();
                        each.OrgMsg = Msg;
                        each.NodeAdr = Msg[1].ToString();
                        string[] content = Msg.Replace("\r", "").Replace("\n", "").Substring(2).Trim().Split('\r');
                        for (int i = 0; i < content.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    each.Type = ReturnMessage.ReturnType.Finished;
                                    each.Value = Msg;
                                    break;

                                default:
                                    each.Type = ReturnMessage.ReturnType.Finished;
                                    each.Value = Msg;
                                    break;
                            }
                        }
                        result.Add(each);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        private List<ReturnMessage> ASYSTCodeAnalysis(string Message)
        {
            List<ReturnMessage> result;
            string[] msgAry;

            try
            {
                result = new List<ReturnMessage>();
                msgAry = Message.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                foreach (string Msg in msgAry)
                {
                    if (Msg.Trim().Equals(""))
                    {
                        continue;
                    }
                    ReturnMessage each = new ReturnMessage();
                    each.OrgMsg = Msg;
                    each.NodeAdr = "00";
                    string[] content = Msg.Replace("\r", "").Replace("\n", "").Split(' ');
                    for (int i = 0; i < content.Length; i++)
                    {
                        switch (each.CommandType)
                        {
                            case "FSD2":
                                switch (content[i].Substring(content[i].IndexOf("=")+1))
                                {
                                    case "F":
                                        each.Value += "1";
                                        break;
                                    case "C":
                                        each.Value += "2";
                                        break;
                                    case "E":
                                        each.Value += "0";
                                        break;
                                    case "U":
                                        each.Value += "?";
                                        break;
                                }
                                each.Type = ReturnMessage.ReturnType.Excuted;
                                break;
                            case "FSD0":
                                if (!each.Value.Equals(""))
                                {
                                    each.Value += ",";
                                }
                                each.Value += content[i];

                                each.Type = ReturnMessage.ReturnType.Excuted;
                                break;
                            default:

                                switch (i)
                                {
                                    case 0:
                                        each.CommandType = content[i];
                                        break;
                                    case 1:
                                        switch (content[i])
                                        {
                                            case "ALARM":
                                            case "ABORT_CAL":
                                            case "ABORT_EMPTY_SLOT":
                                            case "ABORT_HOME":
                                            case "ABORT_LOCK":
                                            case "ABORT_MAP":
                                            case "ABORT_POS":
                                            case "ABORT_SLOT":
                                            case "ABORT_STAGE":
                                            case "ABORT_TWEEKDN":
                                            case "ABORT_TWEEKUP":
                                            case "ABORT_UNLOCK":
                                            case "ABORT_WAFER":
                                            case "WARNING":
                                            case "FATAL":
                                            case "FAILED_SELF-TEST":
                                                each.Type = ReturnMessage.ReturnType.Error;
                                                break;

                                            case "BUSY":
                                            case "DENIED":
                                            case "INVALID_ARG":
                                            case "NO_POD":
                                            case "NOT_READY":
                                                each.Type = ReturnMessage.ReturnType.Abnormal;
                                                break;

                                            case "OK":
                                                each.Type = ReturnMessage.ReturnType.Excuted;
                                                break;

                                            case "CMPL_CAL":
                                            case "CMPL_LOCK":
                                            case "CMPL_MAP":
                                            case "CMPL_SELF-TEST":
                                            case "CMPL_TWEEKDN":
                                            case "CMPL_TWEEKUP":
                                            case "CMPL_UNLOCK":
                                            case "REACH_EMPTY_SLOT":
                                            case "REACH_HOME":
                                            case "REACH_POS":
                                            case "REACH_SLOT":
                                            case "REACH_STAGE":
                                            case "REACH_WAFER":
                                                each.Type = ReturnMessage.ReturnType.Finished;
                                                break;

                                            case "POD_ARRIVED":
                                            case "POD_REMOVED":
                                                each.Type = ReturnMessage.ReturnType.Event;
                                                each.Command = content[i];
                                                break;

                                            default:
                                                each.Command = content[i];
                                                break;
                                        }

                                        break;

                                    case 2:
                                        each.Command = content[i];
                                        break;
                                }
                                break;
                        }
                    }

                    result.Add(each);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        private string TDKFinCommand(string Command)
        {
            string result = "";
            //string strCommsnd = string.Empty;
            string strLen = string.Empty;
            string sCheckSum = string.Empty;
            int chrLH = 0;
            int chrLL = 0;

            try
            {
                Command = "FIN:" + Command + ";";
                strLen = Convert.ToString(Command.Length + 4, 16).PadLeft(2, '0');

                chrLH = Convert.ToInt32(strLen.Substring(0, 1), 16);
                chrLL = Convert.ToInt32(strLen.Substring(1, 1), 16);
                strLen = Convert.ToChar(chrLH).ToString() + Convert.ToChar(chrLL).ToString();
                sCheckSum = TDKCheckSum(strLen, Command);
                result = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", Convert.ToChar(1), strLen, Convert.ToChar(48), string.Empty, Convert.ToChar(48), Command, sCheckSum, Convert.ToChar(3));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return result;
        }

        private string TDKCheckSum(string Len, string Message)
        {
            string strCheckSum = string.Empty;
            string csHex = string.Empty;

            try
            {
                strCheckSum = string.Format("{0}{1}{2}{3}{4}", Len, Convert.ToChar(48), string.Empty, Convert.ToChar(48), Message.ToString());

                byte[] t = new byte[Encoding.ASCII.GetByteCount(strCheckSum)]; ;
                int ttt = Encoding.ASCII.GetBytes(strCheckSum, 0, Encoding.ASCII.GetByteCount(strCheckSum), t, 0);
                byte tt = 0;

                for (int i = 0; i < t.Length; i++)
                {
                    tt += t[i];
                }

                csHex = tt.ToString("X");
                if (csHex.Length == 1)
                {
                    csHex = "0" + csHex;
                }
                return csHex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
