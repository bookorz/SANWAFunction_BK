using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.IO;

namespace SANWA.Utility
{
    public class EncoderAligner
    {
        private string Supplier;
        private DataTable dtRobotCommand;

        /// <summary>
        /// Aligner Encoder
        /// </summary>
        /// <param name="supplier"> 設備供應商 </param>
        /// <param name="dtCommand"> Parameter List </param>
        public EncoderAligner(string supplier, DataTable dtCommand)
        {
            try
            {
                Supplier = supplier;
                dtRobotCommand = dtCommand;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 執行尋找晶圓(Wafer)缺口後移動至所需的角度位置
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="angle"> Align 後 Notch 所要移動的角度 </param>
        /// <returns></returns>
        public string Align(string Address, string Sequence, string angle)
        {
            string Parameter01 = string.Empty;

            if (Supplier == "SANWA")
            {
                Parameter01 = angle;
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = string.Format("{0}.{1}", "A" + Address.ToString(), angle);
            }

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "Angle", Parameter01.Split(','));
        }

        /// <summary>
        /// 暫停解除
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string DeviceContinue(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "Continue");
        }

        /// <summary>
        /// 動作暫停
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string DevicePause(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "Pause");
        }

        /// <summary>
        /// 動作停止
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="m1"></param>
        /// <returns></returns>
        public string DeviceStop(string Address, string Sequence, string m1)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "Stop", m1);
        }

        /// <summary>
        /// Error 履歷取得 
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="no"> 履歷號碼  2 位數  10 進位 </param>
        /// <returns></returns>
        public string ErrorMessage(string Address, string Sequence, string no)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "ErrorList", no);
        }

        /// <summary>
        /// Error 解除
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string ErrorReset(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "ErrorReset");
        }

        /// <summary>
        /// Servo On
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="sv"> Servo ON / OFF 選擇  (0~1) </param>
        /// <returns></returns>
        public string ServoOn(string Address, string Sequence, string sv)
        {
            string Parameter01 = string.Empty;
            string Command = string.Empty;
            string CMD = Supplier == "SANWA" ? "SET" : "CMD";

            if (Supplier == "SANWA")
            {
                Command = "Excitation";
                Parameter01 = sv;
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = "R" + Address.ToString();

                if (sv.Equals("0"))
                {
                    Command = "ExcitationOn";
                }
                else if (sv.Equals("1"))
                {
                    Command = "ExcitationOff";
                }
            }

            return CommandAssembly(Supplier, Address, Sequence, CMD, Command, Parameter01);
        }

        /// <summary>
        /// 各軸移動至 HOME 位置:Normal Home
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Home(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "Home", null);
        }

        /// <summary>
        /// 各軸回 home 的速度回 ORG 的位置，並確認 ORG Sensor
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string HomeOrgin(string Address, string Sequence)
        {
            string Parameter01 = Supplier == "SANWA" ? null : "A" + Address.ToString();

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "HOMEToOrgin", Parameter01);
        }

        /// <summary>
        /// 設定 Log file  儲存
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string LogSave(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "LogSave");
        }

        /// <summary>
        /// 動作模式選擇設定
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="vl"> 動作模式選擇  </param>
        /// <returns></returns>
        public string Mode(string Address, string Sequence, string vl)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "Mode", vl);
        }


        /// <summary>
        /// 取得動作模式選擇設定
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string GetMode(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "Mode", null);
        }

        /// <summary>
        /// 移動指定軸到指定點位
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 軸 (01 ~ 16) </param>
        /// <param name="type"> 移動方式選擇 </param>
        /// <param name="pos"> 移動數量, 9  位數  10 進位(-99999999 ~ +99999999) </param>
        /// <returns></returns>
        public string MoveDirect(string Address, string Sequence, string axis, string type, string pos)
        {
            string Parameter01 = string.Empty;

            if (Supplier == "SANWA")
            {
                Parameter01 = string.Format("{0},{1},{2}", axis, type, pos);
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = string.Format("{0},{1},{2}", "A" + Address.ToString(), axis, pos);
            }

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "MoveDirect", Parameter01.Split(','));
        }

        /// <summary>
        /// 移動到相對位置 - only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 指定上下手臂 </param>
        /// <param name="MoveData"> 移動數值 </param>
        /// <param name="MoveMode"> 移動模式 </param>
        /// <returns></returns>
        public string MoveRelativePosition(string Address, string Sequence, string axis, string MoveData, string MoveMode)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "MoveDirect", "A" + Address.ToString(), axis, MoveData, MoveMode);
        }

        /// <summary>
        /// 原點復歸
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string OrginSearch(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "OrginSearch");
        }

        /// <summary>
        /// 取得 Aligner 參數值
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Type">  參數類別 </param>
        /// <param name="No"> 參數號碼 </param>
        /// <returns></returns>
        public string Parameter(string Address, string Sequence, string Type, string No)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "Parameter", Type, No);
        }

        /// <summary>
        /// 取得 Aligner 參數詳細資料
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Type">  參數類別(0~9) </param>
        /// <param name="No">  參數號碼(000~999) </param>
        /// <returns></returns>
        public string PARSY(string Address, string Sequence, string Type, string No)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "PARSY", Type, No);
        }

        /// <summary>
        /// Arm Retract
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Retract(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "ArmRetract", null);
        }

        /// <summary>
        /// 儲存 Aligner 參數
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Save(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "Save");
        }

        /// <summary>
        /// 設定 Aligner 參數值 
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Type"> 參數類別(0~9) </param>
        /// <param name="No"> 參數號碼(000~999) </param>
        /// <param name="Data"> 設定值(-99999999~+99999999) </param>
        /// <returns></returns>
        public string setParameter(string Address, string Sequence, string Type, string No, string Data)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "Parameter", Type, No, Data);
        }

        /// <summary>
        /// 電磁閥狀態設定
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="no"> 電磁閥號碼 </param>
        /// <param name="vl"> 狀態 </param>
        /// <returns></returns>
        public string setSolenoidValve(string Address, string Sequence, string no, string vl)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "SolenoidValve", no, vl);
        }

        /// <summary>
        /// 速度限制設定
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="vl"> Speed 限制，2 位數 10 進位数 </param>
        /// <returns></returns>
        public string setSpeed(string Address, string Sequence, string vl)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "DeviceStatusSpeed", vl);
        }

        /// <summary>
        /// IO 狀態設定
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="no"> IO 號碼 </param>
        /// <param name="vl"> 狀態 </param>
        /// <returns></returns>
        public string setStatusIO(string Address, string Sequence, string no, string vl)
        {
            string Parameter01 = string.Empty;
            string CMD = Supplier == "SANWA" ? "SET" : "CMD";

            if (Supplier == "SANWA")
            {
                Parameter01 = string.Format("{0}.{1}", no, vl);
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = "A" + Address.ToString();
            }

            return CommandAssembly(Supplier, Address, Sequence, CMD, "DeviceStatusIO", Parameter01.Split(','));
        }

        /// <summary>
        /// 電磁閥狀態取得
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="no"> 電磁閥號碼  (01~10) </param>
        /// <returns></returns>
        public string SolenoidValve(string Address, string Sequence, string no)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "SolenoidValve", no);
        }

        /// <summary>
        /// Speed 限制取得
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Speed(string Address, string Sequence)
        {
            string Parameter01 = string.Empty;
            string CMD = Supplier == "SANWA" ? "GET" : "CMD";

            if (Supplier == "SANWA")
            {
                Parameter01 = null;
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = "A" + Address.ToString();
            }

            return CommandAssembly(Supplier, Address, Sequence, CMD, "DeviceStatusSpeed", Parameter01);
        }

        /// <summary>
        /// Aligner狀態取得
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Status(string Address, string Sequence)
        {
            string CMD = Supplier == "SANWA" ? "GET" : "CMD";
            string Parameter01 = Supplier == "SANWA" ? null : "A" + Address.ToString();

            return CommandAssembly(Supplier, Address, Sequence, CMD, "DeviceStatus", Parameter01);
        }

        /// <summary>
        /// Aligner 合併狀態取得 only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string CombinedStatus(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "CombinedDeviceStatus", "A" + Address.ToString());
        }

        /// <summary>
        /// IO 狀態取得
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="nol"> IO 號碼 </param>
        /// <returns></returns>
        public string StatusIO(string Address, string Sequence, string nol)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "DeviceStatusIO", nol);
        }

        /// <summary>
        /// Step 動作等待解除
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string STPDO(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "STPDO");
        }

        /// <summary>
        /// 保持晶圓控制
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string WaferHold(string Address, string Sequence)
        {
            string Parameter01 = Supplier == "SANWA" ? "1" : "A" + Address.ToString();

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "WaferHold", Parameter01.Split(','));
        }

        /// <summary>
        /// 釋放晶圓控制
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string WaferReleaseHold(string Address, string Sequence, string arm)
        {
            string Parameter01 = Supplier == "SANWA" ? "1" : "A" + Address.ToString();

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "WaferRelease", Parameter01.Split(','));
        }

        private string CommandAssembly(string Supplier, string Address, string Sequence, string CommandType, string Command, params string[] Parameter)
        {
            string strCommand = string.Empty;
            string strCommandFormat = string.Empty;
            string strCommandFormatParameter = string.Empty;
            DataTable dtTemp;
            DataView dvTemp;
            ContainerSet container;
            StringBuilder sbTemp;
            string[] strsParameter;

            try
            {

                container = new ContainerSet();
                sbTemp = new StringBuilder();

                var query = (from a in dtRobotCommand.AsEnumerable()
                             where a.Field<string>("node_type") == "ALIGNER"
                                && a.Field<string>("vendor") == Supplier
                                && a.Field<string>("code_type") == CommandType
                                && a.Field<string>("Action_Function") == Command
                             select a).ToList();

                if (query.Count == 0)
                {
                    throw new RowNotInTableException();
                }

                dtTemp = query.CopyToDataTable();
                dtTemp.DefaultView.Sort = "Parameter_Order ASC";
                dvTemp = dtTemp.DefaultView;

                switch (Supplier)
                {
                    case "SANWA":

                        if (Parameter !=null && Parameter.Length != 0)
                        {
                            if ((dvTemp.Table.Rows[0]["Parameter_ID"].ToString().Equals("Null")) && (Parameter.Length != dtTemp.Rows.Count))
                            {
                                sbTemp.Append("Equipment Type : Robot");
                                sbTemp.AppendFormat("Equipment Supplier : {0}", Supplier);
                                sbTemp.AppendFormat("Command Type : {0}", CommandType);
                                sbTemp.AppendFormat("Command : {0}", Command);
                                sbTemp.Append("Parameter list and setting list are not the same.");
                                throw new Exception(sbTemp.ToString());
                            }
                        }

                        strsParameter = Parameter;

                        for (int i = 0; i < dvTemp.Table.Rows.Count; i++)
                        {
                            Int32 itTemp = 0;

                            if (!dvTemp.Table.Rows[i]["Parameter_ID"].ToString().Equals("Null"))
                            {
                                if (int.Parse(dvTemp.Table.Rows[i]["Min_Value"].ToString()) > int.Parse(Parameter[i].ToString()))
                                {
                                    throw new Exception("Exceed the minimum.");
                                }

                                if (int.Parse(dvTemp.Table.Rows[i]["Max_Value"].ToString()) < int.Parse(Parameter[i].ToString()))
                                {
                                    throw new Exception("Exceed the maximum.");
                                }

                                if (dvTemp.Table.Rows[i]["Data_Value"].ToString().Equals(string.Empty))
                                {
                                    if (dvTemp.Table.Rows[i]["Is_Fill"].ToString().Equals("Y"))
                                    {
                                        itTemp = int.Parse(Parameter[i].ToString());

                                        if (Command.Equals("Angle"))
                                        {
                                            strsParameter[i] = itTemp.ToString().PadRight(6, '0');
                                        }
                                        else
                                        {
                                            strsParameter[i] = itTemp.ToString("D" + dvTemp.Table.Rows[i]["Values_length"].ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    if (dvTemp.Table.Rows[i]["Data_Value"].ToString().IndexOf(Parameter[i].ToString()) < 0)
                                    {
                                        throw new Exception(dvTemp.Table.Rows[i]["Parameter_ID"].ToString() + ": Setting value error.");
                                    }
                                }
                            }
                        }

                        strCommandFormat = container.StringFormat(dtTemp.Rows[0]["code_format"].ToString(), new string[] { Address, Sequence });

                        if (strsParameter != null &&  strsParameter.Length != 0)
                        {
                            strCommandFormatParameter = container.StringFormat(dtTemp.Select(), strsParameter);
                        }

                        break;

                    case "KAWASAKI":

                        if (Parameter != null && Parameter.Length != 0)
                        {
                            if ((dvTemp.Table.Rows[0]["Parameter_ID"].ToString().Equals("Null") || dvTemp.Table.Rows[0]["Parameter_ID"].ToString().Equals("Data") || dvTemp.Table.Rows[0]["Parameter_ID"].ToString().Equals("DateTime")
                                ) && (Parameter.Length != dtTemp.Rows.Count))
                            {
                                sbTemp.Append("Equipment Type : Robot");
                                sbTemp.AppendFormat("Equipment Supplier : {0}", Supplier);
                                sbTemp.AppendFormat("Command Type : {0}", CommandType);
                                sbTemp.AppendFormat("Command : {0}", Command);
                                sbTemp.Append("Parameter list and setting list are not the same.");
                                throw new Exception(sbTemp.ToString());
                            }
                        }

                        strsParameter = Parameter;

                        for (int i = 0; i < dvTemp.Table.Rows.Count; i++)
                        {
                            Int32 itTemp = 0;

                            if (dvTemp.Table.Rows[i]["Parameter_ID"].ToString().Equals("Null") && !dvTemp.Table.Rows[i]["Parameter_ID"].ToString().Equals("Data"))
                            {
                                // * Value mode
                                if (dvTemp.Table.Rows[i]["Data_Value"].ToString().Equals(string.Empty))
                                {
                                    if (int.Parse(dvTemp.Table.Rows[i]["Min_Value"].ToString()) > int.Parse(Parameter[i].ToString()))
                                    {
                                        throw new Exception("Exceed the minimum.");
                                    }

                                    if (int.Parse(dvTemp.Table.Rows[i]["Max_Value"].ToString()) < int.Parse(Parameter[i].ToString()))
                                    {
                                        throw new Exception("Exceed the maximum.");
                                    }

                                    if (dvTemp.Table.Rows[i]["Data_Value"].ToString().Equals(string.Empty))
                                    {
                                        if (dvTemp.Table.Rows[i]["Is_Fill"].ToString().Equals("Y"))
                                        {
                                            itTemp = int.Parse(Parameter[i].ToString());
                                            strsParameter[i] = itTemp.ToString("D" + dvTemp.Table.Rows[i]["Values_length"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        if (dvTemp.Table.Rows[i]["Data_Value"].ToString().IndexOf(Parameter[i].ToString()) < 0)
                                        {
                                            throw new Exception(dvTemp.Table.Rows[i]["Parameter_ID"].ToString() + ": Setting value error.");
                                        }
                                    }
                                }
                                // * string mode
                                else
                                {
                                    if (dvTemp.Table.Rows[i]["Data_Value"].ToString().IndexOf(strsParameter[i].ToString()) < 0)
                                    {
                                        throw new Exception(dvTemp.Table.Rows[i]["Data_Value"].ToString() + ": Out of setting range.");
                                    }
                                }

                                if (dvTemp.Table.Rows[i]["Parameter_ID"].ToString().Equals("DateTime"))
                                {
                                    strsParameter[i] = Convert.ToDateTime(strsParameter[i]).ToString("yy/MM/dd HH:mm:ss");
                                }
                            }
                        }

                        sbTemp = new StringBuilder();
                        sbTemp = new StringBuilder();
                        if (strsParameter != null)
                        {
                            for (int i = 0; i < strsParameter.Length; i++)
                            {
                                sbTemp.Append(strsParameter[i].ToString());
                                sbTemp.Append(",");

                            }
                        }

                        strCommandFormat = container.StringFormat(dtTemp.Rows[0]["code_format"].ToString(), new string[] { sbTemp.ToString().TrimEnd(','), strCommandFormatParameter });


                        break;

                    default:
                        throw new NotImplementedException();
                }

                strCommand = strCommandFormat + strCommandFormatParameter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return strCommand + "\r";
        }

        private string KawasakiCheckSum(string Parameter)
        {
            string strCheckSum = string.Empty;
            string strLen = string.Empty;
            int chrLH = 0;
            int chrLL = 0;

            try
            {
                byte[] asc = new byte[Encoding.ASCII.GetByteCount(Parameter)];
                byte ascCount = 0;

                for (int i = 0; i < asc.Length; i++)
                {
                    ascCount += asc[i];
                }

                strLen = Convert.ToString(ascCount % 265).PadLeft(2, '0');
                chrLH = Convert.ToInt32(strLen.Substring(0, 1), 16);
                chrLL = Convert.ToInt32(strLen.Substring(1, 1), 16);

                strCheckSum = Convert.ToChar(chrLH).ToString() + Convert.ToChar(chrLL).ToString();

                return strCheckSum;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
