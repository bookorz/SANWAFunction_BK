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
                Parameter01 = string.Format("{0}.{1}", Address.ToString(), angle);
            }

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "Angle", Parameter01);
        }

        /// <summary>
        /// 執行尋找晶圓(Wafer)缺口後移動至所需的角度位置 [ SANWA ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="angle"> Align 後 Notch 所要移動的角度  </param>
        /// <param name="notch"> Align 尋邊動作模式 
        /// <para> 0 : 根據上一次align結果，不再做尋邊動作，直接到notch/flat指定角度，並補正偏心，不先回home </para>
        /// <para> 1 : normal mode </para>
        /// <para> 2 : 只做尋邊動作，aligner不做到notch/flat指定角度與補正偏心 </para> </param>
        /// <param name="ZAxis"> Z軸動作模式
        /// <para> 0 : 無Z軸 </para>
        /// <para> 1 : align完成Z軸下降 </para>
        /// <para> 2 : align完成Z軸上升 </para> </param>                     
        /// <param name="mode"> 執行模式          
        /// <para> 0 快速模式，尋邊1圈，並以最短路徑到notch/flat指定角度，並補正偏心 </para>
        /// <para> 1 normal mode  </para> </param>
        /// <returns></returns>
        public string Align(string Address, string Sequence, string angle, string notch, string ZAxis, string mode)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "Align", angle, notch, ZAxis, mode);
        }

        /// <summary>
        /// 設定晶圓(Wafer)大小
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="vl"> vl：Wafer Size 的半徑(um)，150000 代表 12 吋 Wafer，100000 代表 8 吋 Wafer </param>
        /// <returns></returns>
        public string SetSize(string Address, string Sequence, string vl)
        {
            string Parameter01 = string.Empty;

            if (Supplier == "SANWA")
            {
                Parameter01 = vl;
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = string.Format("{0}.{1}", Address.ToString(), vl);
            }

            return CommandAssembly(Supplier, Address, Sequence, "SET", "SetSize", Parameter01.Split(','));
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
            string CMD = Supplier == "SANWA" ? "SET" : "SET";

            if (Supplier == "SANWA")
            {
                Command = "Excitation";
                Parameter01 = sv;
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = Address.ToString();

                if (sv.Equals("1"))
                {
                    Command = "ExcitationOn";
                }
                else if (sv.Equals("0"))
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
            string Parameter01 = Supplier == "SANWA" ? null : Address.ToString();

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
            string Parameter01 = string.Empty;
            string CMD = Supplier == "SANWA" ? "SET" : "SET";

            if (Supplier == "SANWA")
            {
                Parameter01 = vl;
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = vl;
            }

            return CommandAssembly(Supplier, Address, Sequence, CMD, "Mode", Parameter01.Split(','));
        }

        /// <summary>
        /// 取得動作模式選擇設定
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string GetMode(string Address, string Sequence)
        {
            string CMD = Supplier == "SANWA" ? "GET" : "GET";
            string CMDType = Supplier == "SANWA" ? "Mode" : "ModeGet";
            return CommandAssembly(Supplier, Address, Sequence, CMD, CMDType, null);
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
                Parameter01 = string.Format("{0},{1},{2}", Address.ToString(), axis, pos);
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
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "MoveDirect", Address.ToString(), axis, MoveData, MoveMode);
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
            string Parameter01 = string.Empty;
            string CMD = Supplier == "SANWA" ? "SET" : "SET";

            if (Supplier == "SANWA")
            {
                Parameter01 = vl;
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = string.Format("{0},{1}", Address.ToString(), vl);
            }

            return CommandAssembly(Supplier, Address, Sequence, CMD, "DeviceStatusSpeed", Parameter01.Split(','));
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
                Parameter01 = Address.ToString();
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
            string CMD = Supplier == "SANWA" ? "GET" : "GET";

            if (Supplier == "SANWA")
            {
                //Parameter01 = null;
                Parameter01 = "";//20180614 fix null exception
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = Address.ToString();
            }

            return CommandAssembly(Supplier, Address, Sequence, CMD, "DeviceStatusSpeed", Parameter01.Split(','));
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
            string Parameter01 = Supplier == "SANWA" ? null : Address.ToString();

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
            return CommandAssembly(Supplier, Address, Sequence, "GET", "CombinedDeviceStatus", Address.ToString());
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
            string Parameter01 = Supplier == "SANWA" ? "1" : Address.ToString();

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "WaferHold", Parameter01.Split(','));
        }

        /// <summary>
        /// 釋放晶圓控制
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string WaferReleaseHold(string Address, string Sequence)
        {
            string Parameter01 = Supplier == "SANWA" ? "1" : Address.ToString();

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "WaferRelease", Parameter01.Split(','));
        }

        /// <summary>
        /// Aligner Offset
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string AlignerOffset(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "AlignerOffset", Address.ToString());
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
                                // * 16進位比對
                                if (dvTemp.Table.Rows[i]["Parameter_ID"].ToString().Equals("HEX"))
                                {
                                    if (int.Parse(dvTemp.Table.Rows[i]["Min_Value"].ToString()) > Convert.ToInt32(Parameter[i].ToString(), 16))
                                    {
                                        throw new Exception("Exceed the minimum.");
                                    }

                                    if (int.Parse(dvTemp.Table.Rows[i]["Max_Value"].ToString()) < Convert.ToInt32(Parameter[i].ToString(), 16))
                                    {
                                        throw new Exception("Exceed the maximum.");
                                    }
                                }
                                else
                                {
                                    if (int.Parse(dvTemp.Table.Rows[i]["Min_Value"].ToString()) > int.Parse(Parameter[i].ToString()))
                                    {
                                        throw new Exception("Exceed the minimum.");
                                    }

                                    if (int.Parse(dvTemp.Table.Rows[i]["Max_Value"].ToString()) < int.Parse(Parameter[i].ToString()))
                                    {
                                        throw new Exception("Exceed the maximum.");
                                    }
                                }

                                if (dvTemp.Table.Rows[i]["Data_Value"].ToString().Equals(string.Empty))
                                {
                                    if (dvTemp.Table.Rows[i]["Is_Fill"].ToString().Equals("Y"))
                                    {
                                        itTemp = int.Parse(Parameter[i].ToString());

                                        strsParameter[i] = itTemp.ToString("D" + dvTemp.Table.Rows[i]["Values_length"].ToString());

                                        if (i == 0 && (Command.Equals("Angle") || Command.Equals("Align")))
                                        {
                                            strsParameter[i] = strsParameter[i] + "000";
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

                            if (!dvTemp.Table.Rows[i]["Parameter_ID"].ToString().Equals("Null") && !dvTemp.Table.Rows[i]["Parameter_ID"].ToString().Equals("Data"))
                            {
                                // * Value mode
                                if (dvTemp.Table.Rows[i]["Data_Value"].ToString().TrimEnd().Equals(string.Empty))
                                {
                                    // * 16進位比對
                                    if (dvTemp.Table.Rows[i]["Parameter_ID"].ToString().Equals("HEX"))
                                    {
                                        if (int.Parse(dvTemp.Table.Rows[i]["Min_Value"].ToString()) > Convert.ToInt32(Parameter[i].ToString(), 16))
                                        {
                                            throw new Exception("Exceed the minimum.");
                                        }

                                        if (int.Parse(dvTemp.Table.Rows[i]["Max_Value"].ToString()) < Convert.ToInt32(Parameter[i].ToString(), 16))
                                        {
                                            throw new Exception("Exceed the maximum.");
                                        }
                                    }
                                    else
                                    {
                                        if (int.Parse(dvTemp.Table.Rows[i]["Min_Value"].ToString()) > int.Parse(Parameter[i].ToString()))
                                        {
                                            throw new Exception("Exceed the minimum.");
                                        }

                                        if (int.Parse(dvTemp.Table.Rows[i]["Max_Value"].ToString()) < int.Parse(Parameter[i].ToString()))
                                        {
                                            throw new Exception("Exceed the maximum.");
                                        }
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

                        if (strsParameter != null)
                        {
                            for (int i = 0; i < strsParameter.Length; i++)
                            {
                                sbTemp.Append(strsParameter[i].ToString());
                                sbTemp.Append(",");
                            }

                            strCommandFormat = container.StringFormat(dtTemp.Rows[0]["code_format"].ToString().Insert(1, Sequence + ","), new string[] { ",", sbTemp.ToString().TrimEnd(',') });
                            strCommandFormat = strCommandFormat + KawasakiCheckSum(Sequence + "," + dtTemp.Rows[0]["code_id"].ToString() + "," + sbTemp.ToString().TrimEnd(','));
                        }
                        else
                        {
                            strCommandFormat = container.StringFormat(dtTemp.Rows[0]["code_format"].ToString().Insert(1, Sequence + ","), new string[] { string.Empty, string.Empty });
                            strCommandFormat = strCommandFormat + KawasakiCheckSum(Sequence + "," + dtTemp.Rows[0]["code_id"].ToString());
                        }

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

            switch (Supplier)
            {
                case "SANWA":
                    strCommand = strCommand + "\r";
                    break;

                case "KAWASAKI":
                    strCommand = strCommand + "\r\n";
                    break;
            }

            return strCommand;
        }

        private string KawasakiCheckSum(string Parameter)
        {
            string strCheckSum = string.Empty;
            int value = 0;
            int sum = 0;
            int remainder = 0;
            char[] charValues;

            try
            {
                charValues = Parameter.ToCharArray();

                foreach (char _eachChar in charValues)
                {
                    value = Convert.ToInt32(_eachChar);
                    sum = sum + Convert.ToInt32(_eachChar);
                }

                remainder = sum % 256;

                strCheckSum = String.Format("{0:X}", remainder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            if (strCheckSum.Length == 1)
            {
                strCheckSum = "0" + strCheckSum;
            }
            return strCheckSum;
        }
    }
}
