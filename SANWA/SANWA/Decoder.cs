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
                    case "SANWACONTROLLER":
                        result = SANWACodeAnalysis(Message);
                        break;

                    case "TDKCONTROLLER":
                        result = TDKCodeAnalysis(Message);
                        break;

                    case "KAWASAKICONTROLLER":
                        result = KAWASAKICodeAnalysis(Message);
                        break;
                    case "HSTCONTROLLER":
                        result = HSTCodeAnalysis(Message);
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

        private List<ReturnMessage> HSTCodeAnalysis(string Message)
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
                    switch (Msg)
                    {
                        case "1": 
                        case "0":
                            each.Type = ReturnMessage.ReturnType.Excuted;
                            each.Value = Msg;
                            break;
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
                                }
                                each.CommandType = content[i];
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
                                if(each.Type == ReturnMessage.ReturnType.Finished && !each.Value.Equals("00000000"))
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

                    each.Command = Msg.Substring(Msg.IndexOf('<') + 1, Msg.IndexOf('>') - Msg.IndexOf('<') - 1);
                    string[] content = each.Command.Split(',');
                    for (int i = 0; i < content.Length; i++)
                    {
                        switch (i)
                        {
                            case 1:

                                switch (content[i])
                                {
                                    case "Nak":
                                        each.Type = ReturnMessage.ReturnType.Abnormal;
                                        each.Value = content[2];
                                        break;
                                    case "Success":
                                        each.Type = ReturnMessage.ReturnType.Excuted;
                                        each.Value = content[2];
                                        break;
                                    case "Error":
                                        each.Type = ReturnMessage.ReturnType.Error;
                                        each.NodeAdr = content[3].ToString();
                                        each.Value = content[2] + ":" + content[4];
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
                                    each.Type = ReturnMessage.ReturnType.Information;
                                    break;
                                case "EVT":
                                    each.Type = ReturnMessage.ReturnType.Event;
                                    break;
                                case "ABS":
                                    each.Type = ReturnMessage.ReturnType.Abnormal;
                                    break;
                                case "RIF":
                                    each.Type = ReturnMessage.ReturnType.ReInformation;
                                    break;
                            }
                            break;
                        case 1:

                            each.Command = content[i];
                            if (each.Type == ReturnMessage.ReturnType.Information || each.Type == ReturnMessage.ReturnType.ReInformation)
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

                return csHex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
