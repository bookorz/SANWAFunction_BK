using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.IO;

namespace SANWA.Utility
{
    public class EncoderRobot
    {
        private string Supplier;
        private DataTable dtRobotCommand;

        /// <summary>
        /// Robot Encoder
        /// </summary>
        /// <param name="supplier"> 設備供應商 </param>
        /// <param name="dtCommand"> Parameter List </param>
        public EncoderRobot(string supplier, DataTable dtCommand)
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
        /// Abssolute Encoder Offset
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 軸，(01~16) </param>
        /// <returns></returns>
        public string AbssoluteEncoderOffset(string Address, string Sequence, string axis)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "AbssoluteEncoderOffset", axis);
        }

        /// <summary>
        /// Abssolute Encoder Reset
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 軸，(01~16) </param>
        /// <returns></returns>
        public string AbssoluteEncoderReset(string Address, string Sequence, string axis)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "AbssoluteEncoderReset", axis);
        }

        /// <summary>
        /// 取得  R  軸到  R1  軸目前位置
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Type">  Command/Encoder  選擇 </param>
        /// <param name="Unit"> 單位選擇 </param>
        /// <returns></returns>
        public string ArmLocation(string Address, string Sequence, string Type, string Unit)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "ArmLocation", Type, Unit);
        }

        /// <summary>
        /// Battery Alarm Clear
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 軸，(01~16) </param>
        /// <returns></returns>
        public string BatteryAlarmClear(string Address, string Sequence, string axis)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "BatteryAlarmClear", axis);
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
        /// 動作停止 [ SANWA, ATEL ]
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
        /// Error 履歷取得  [ SANWA, KAWASAKI, ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="no"> 履歷號碼  </param>
        /// <returns></returns>
        public string ErrorMessage(string Address, string Sequence, string no)
        {
            string Parameter01 = string.Empty;

            if (Supplier == "KAWASAKI")
            {
                Parameter01 = string.Format("{0},{1}", Address.ToString(), no);
            }
            else
            {
                Parameter01 = no;
            }

            return CommandAssembly(Supplier, Address, Sequence, "GET", "ErrorList", Parameter01.Split(','));
        }

        /// <summary>
        /// Error 解除 [ SANWA, ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string ErrorReset(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "ErrorReset");
        }

        /// <summary>
        /// 取片+放片  動作指令(Exchange)    : Carry
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm_form"> Arm 選擇 ( 1~2 ) </param>
        /// <param name="Point_form"> Point 編號 </param>
        /// <param name="Slot_form"> Slot 編號 </param>
        /// <param name="Arm_to"> Arm 選擇 ( 1~2 ) </param>
        /// <param name="Point_to"> Point 編號 </param>
        /// <param name="Slot_to"> Slot 編號 </param>
        /// <returns></returns>
        public string Exchange(string Address, string Sequence, string Arm_form, string Point_form, string Slot_form, string Arm_to, string Point_to, string Slot_to)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "Exchange", new string[] { Point_form, Slot_form, Arm_form, Point_to, Slot_to, Arm_to });
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

            if (Supplier == "SANWA" || Supplier == "ATEL_NEW")
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
        /// 取片 [ SANWA, KAWASAKI, ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇  ( 1~3) </param>
        /// <param name="Point"> Point 編號，4 位數 10 進位 </param>
        /// <param name="Alignment"> Alignment 選擇 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string GetWafer(string Address, string Sequence, string Arm, string Point, string Alignment, string Slot)
        {
            string strMsg = string.Empty;
            string Parameter01 = string.Empty;

            switch (Supplier)
            {
                case "SANWA":

                    Parameter01 = string.Format("{0},{1},{2},{3},{4}", Point, Slot, Arm, Alignment, "0");
                    strMsg = CommandAssembly(Supplier, Address, Sequence, "CMD", "GetWafer", Parameter01.Split(','));
                    break;
                case "ATEL_NEW":
                    Parameter01 = string.Format("{0},{1},{2},{3}", Point, Slot, Arm, "0");
                    strMsg = CommandAssembly(Supplier, Address, Sequence, "CMD", "GetWafer", Parameter01.Split(','));
                    break;
                case "KAWASAKI":
                    Parameter01 = string.Format("{0},{1},{2},{3}", Address.ToString(), Arm, Point, Slot);
                    strMsg = CommandAssembly(Supplier, Address, Sequence, "CMD", "GetWafer", Parameter01.Split(','));
                    break;

                case "ATEL":
                    strMsg = RunMacro(Address, Sequence, GetMacroValue(Address, "IGET", Arm, Point, Slot));
                    break;
            }

            return strMsg;
        }

        /// <summary>
        /// 取片 (SANWA)- 繼續執行 1 or 2 後續的動作 (必須前一個指令有下過 1 或是 2 的 option)
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇  ( 1~3) </param>
        /// <param name="Point"> Point 編號，4 位數 10 進位 </param>
        /// <param name="Alignment"> Alignment 選擇 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string GetWaferToContinue(string Address, string Sequence, string Arm, string Point, string Alignment, string Slot)
        {
            switch (Supplier)
            {

                case "ATEL_NEW":
                    return CommandAssembly(Supplier, Address, Sequence, "CMD", "GetWafer", Point, Slot, Arm, "3");

                default:
                    return CommandAssembly(Supplier, Address, Sequence, "CMD", "GetWafer", Point, Slot, Arm, Alignment, "3");
            }
        }

        /// <summary>
        /// 取片 SXZ  軸到位後停止: Get Wait
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇  ( 1~3) </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string GetWaferToReady(string Address, string Sequence, string Arm, string Point, string Slot)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "Get_ArmToReady", Point, Slot, Arm);
        }

        /// <summary>
        /// 取片 (SANWA)- Z  軸移動至  Panel  下點位，Arm 伸出後就停止動作
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇  ( 1~3) </param>
        /// <param name="Point"> Point 編號，4 位數 10 進位 </param>
        /// <param name="Alignment"> Alignment 選擇 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string GetWaferToStandBy(string Address, string Sequence, string Arm, string Point, string Alignment, string Slot)
        {
            switch (Supplier)
            {

                case "ATEL_NEW":
                    return CommandAssembly(Supplier, Address, Sequence, "CMD", "GetWafer", Point, Slot, Arm, "1");
             
                default:
                    return CommandAssembly(Supplier, Address, Sequence, "CMD", "GetWafer", Point, Slot, Arm, Alignment, "1");
              
            }
        }

        /// <summary>
        /// 取片 (SANWA)- Arm 伸出後，Z  軸上升至取片位置後就停止動作
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇  ( 1~3) </param>
        /// <param name="Point"> Point 編號，4 位數 10 進位 </param>
        /// <param name="Alignment"> Alignment 選擇 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string GetWaferToUp(string Address, string Sequence, string Arm, string Point, string Alignment, string Slot)
        {
            switch (Supplier)
            {

                case "ATEL_NEW":
                    return CommandAssembly(Supplier, Address, Sequence, "CMD", "GetWafer", Point, Slot, Arm, "2");

                default:
                    return CommandAssembly(Supplier, Address, Sequence, "CMD", "GetWafer", Point, Slot, Arm, Alignment, "2"); ;
            }
        }

        /// <summary>
        /// 各軸移動至 HOME 位置:Normal Home [ SANWA, KAWASAKI ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Home(string Address, string Sequence)
        {
            string Parameter01 = string.Empty;

            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    Parameter01 = null;
                    break;

                case "KAWASAKI":
                    Parameter01 = Address.ToString();
                    break;
            }

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "Home", Parameter01);
        }

        /// <summary>
        /// 指定 軸移動至 HOME 位置:Normal Home [ ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Home(string Address, string Sequence, string axis, string speed)
        {
            string Parameter01 = string.Empty;

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "Home", axis, speed);
        }

        /// <summary>
        /// 各軸回 home 的速度回 ORG 的位置，並確認 ORG Sensor
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string HomeOrgin(string Address, string Sequence)
        {
            string Parameter01 = Supplier == "KAWASAKI" ?  Address.ToString() :null;

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "HOMEToOrgin", Parameter01);
        }

        /// <summary>
        /// 各軸安全回 HOME 位置: Safety Home
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string HomeSafety(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "HOMEToSafety", null);
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
        /// Mapping 結果取得
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="no"> Data 選擇  (1~3) </param>
        /// <returns></returns>
        public string MapList(string Address, string Sequence, string no)
        {
            string Parameter01 = string.Empty;
            string CMD = Supplier == "SANWA" ? "GET" : "GET";
            string Command = Supplier == "KAWASAKI" ? "MappingGet" : "Mapping";

            if (Supplier == "SANWA" || Supplier == "ATEL_NEW")
            {
                Parameter01 = no;
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = string.Format("{0},{1}", Address.ToString(), no);
            }

            return CommandAssembly(Supplier, Address, Sequence, CMD, Command, Parameter01.Split(','));
        }

        /// <summary>
        /// Mapping
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Point Data 編號。(0001 ~ 1999) </param>
        /// <param name="col"> Cassette  列編號 </param>
        /// <param name="slot"> Slot  編號。 </param>
        /// <returns></returns>
        public string Mapping(string Address, string Sequence, string pno, string col, string slot)
        {
            string Parameter01 = string.Empty;

            if (Supplier == "SANWA" || Supplier == "ATEL_NEW")
            {
                Parameter01 = string.Format("{0},{1},{2}", pno, col, slot);
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = string.Format("{0},{1}", Address.ToString(), pno);
            }

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "Mapping", Parameter01.Split(','));
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

            if (Supplier == "SANWA" || Supplier == "ATEL_NEW")
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
            //string CMDType = Supplier == "SANWA" ? "Mode" : "ModeGet";
            string CMDType = Supplier == "KAWASAKI" ? "ModeGet" : "Mode";
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

            if (Supplier == "SANWA"|| Supplier == "ATEL_NEW")
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
        /// 移動到指定的位置 - only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 指定上下手臂 </param>
        /// <param name="pos"> 點位 </param>
        /// <param name="Slot"> Slot </param>
        /// <param name="LocationCode"> Location Code </param>
        /// <returns></returns>
        public string MovePosition(string Address, string Sequence, string axis, string Pos, string Slot, string LocationCode)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "MovePosition", Address.ToString(), axis, Pos, Slot, LocationCode);
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
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "MoveRelativePosition", Address.ToString(), axis, MoveData, MoveMode);
        }

        /// <summary>
        /// Multi Panel  選擇命令
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="arm"> 1 或 2，R 或Ｌ軸選擇 </param>
        /// <param name="no1"> 選擇指定的 Panel </param>
        /// <param name="no2"> 選擇指定的 Panel /param>
        /// <param name="no3"> 選擇指定的 Panel /param>
        /// <param name="no4"> 選擇指定的 Panel /param>
        /// <param name="no5"> 選擇指定的 Panel /param>
        /// <param name="no6"> 選擇指定的 Panel /param>
        /// <param name="no7"> 選擇指定的 Panel /param>
        /// <param name="no8"> 選擇指定的 Panel /param>
        /// <param name="no9"> 選擇指定的 Panel /param>
        /// <param name="no10"> 選擇指定的 Panel </param>
        /// <param name="no11"> 選擇指定的 Panel </param>
        /// <param name="no12"> 選擇指定的 Panel </param>
        /// <param name="no13"> 選擇指定的 Panel </param>
        /// <param name="no14"> 選擇指定的 Panel </param>
        /// <param name="no15"> 選擇指定的 Panel </param>
        /// <param name="no16"> 選擇指定的 Panel </param>
        /// <returns></returns>
        public string MultiPanel(string Address, string Sequence, string arm, string no1, string no2, string no3, string no4, string no5, string no6, string no7, string no8, string no9, string no10, string no11, string no12, string no13, string no14, string no15, string no16)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "MultiPanel", arm, no1, no2, no3, no4, no5, no6, no7, no8, no9, no10, no11, no12, no13, no14, no15, no16);
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
        /// 單軸 Orgin Search [ SANWA, ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 軸, (01 ~ 16) </param>
        /// <returns></returns>
        public string OrginSearchByAxis(string Address, string Sequence, string axis)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "OrginSearchSingle", axis);
        }

        /// <summary>
        /// 取得 Robot 參數值
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Type">  參數類別 </param>
        /// <param name="No"> 參數號碼 </param>
        /// <returns></returns>
        public string Parameter(string Address, string Sequence, string Type, string No)
        {
            string CMD = Supplier == "SANWA" ? "GET" : "GET";
            string Command = Supplier == "KAWASAKI" ? "ParameterGet" : "Parameter";

            string Parameter01 = string.Empty;

            if (Supplier == "SANWA" || Supplier == "ATEL_NEW")
            {
                Parameter01 = string.Format("{0},{1}", Type, No);
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = No;
            }

            return CommandAssembly(Supplier, Address, Sequence, CMD, Command, Parameter01.Split(','));
        }

        /// <summary>
        /// 設定 Robot 指定站點參數
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Point"> Station Code </param>
        /// <param name="No"> Parameter Name </param>
        /// <param name="Data"> Parameter Data </param>
        /// <returns></returns>
        public string setParameterStation(string Address, string Sequence, string Point, string No, string Data)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "ParameterStation", Address.ToString(), Point, No, Data);
        }

        /// <summary>
        /// 取回 Robot 指定站點參數
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Point"> Station Code </param>
        /// <param name="No"> Parameter Name </param>
        /// <returns></returns>
        public string ParameterStation(string Address, string Sequence, string Point, string No)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "ParameterStationGet", Address.ToString(), Point, No);
        }

        /// <summary>
        /// 取得 Robot 參數詳細資料
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
        /// 將 Point Data  從 CF 卡載入
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string PointLoad(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "PointLoad");
        }

        /// <summary>
        /// 將 Point Data  存入  CF 卡
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string PointSave(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "PointSave");
        }

        /// <summary>
        /// 取回各軸位置 only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string CurrentPosition(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "CurrentPosition", Address.ToString());
        }

        /// <summary>
        /// 取回座標位置 only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> 手臂 </param>
        /// <returns></returns>
        public string CurrentCoordinatePosition(string Address, string Sequence, string Arm)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "CurrentCoordinatePosition", Address.ToString(), Arm);
        }

        /// <summary>
        /// 取回當前機器人最近的位置和Slot only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> 手臂 </param>
        /// <returns></returns>
        public string NearestStation(string Address, string Sequence, string Arm)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "NearestStation", Address.ToString(), Arm);
        }

        /// <summary>
        /// 放片 [ SANWA, KAWASAKI, ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇 </param>
        /// <param name="Point"> Point 編號 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string PutWafer(string Address, string Sequence, string Arm, string Point, string Slot)
        {
            string strMsg = string.Empty;
            string Parameter01 = string.Empty;

            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    Parameter01 = string.Format("{0},{1},{2},{3}", Point, Slot, Arm, "0");
                    strMsg = CommandAssembly(Supplier, Address, Sequence, "CMD", "PutWafer", Parameter01.Split(','));
                    break;

                case "KAWASAKI":
                    Parameter01 = string.Format("{0},{1},{2},{3}", Address.ToString(), Arm, Point, Slot);
                    strMsg = CommandAssembly(Supplier, Address, Sequence, "CMD", "PutWafer", Parameter01.Split(','));
                    break;

                case "ATEL":
                    strMsg = RunMacro(Address, Sequence, GetMacroValue(Address, "IPUT", Arm, Point, Slot));
                    break;
            }

            return strMsg; 
        }

        /// <summary>
        /// 放片 (SANWA) - 繼續執行 1 or 2  後續的動作 (必須前一個指令有下過  1  或是  2 的 option)
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇 </param>
        /// <param name="Point"> Point 編號 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string PutWaferToContinue(string Address, string Sequence, string Arm, string Point, string Slot)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "PutWafer", Point, Slot, Arm, "3");
        }

        /// <summary>
        /// 放片 (SANWA) - Z 中間位置停止(Teaching 位置  + Z 軸下降設定距離)
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇 </param>
        /// <param name="Point"> Point 編號 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string PutWaferToDown(string Address, string Sequence, string Arm, string Point, string Slot)
        {

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "PutWafer", Point, Slot, Arm, "2");
        }

        /// <summary>
        /// 取片 SXZ  軸到位後停止: Get Wait
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇  ( 1~2 ) </param>
        /// <param name="Point"> Point 編號 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string PutWaferToReady(string Address, string Sequence, string Arm, string Point, string Slot)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "Put_ArmToReady", Point, Slot, Arm);
        }

        /// <summary>
        /// 放片 (SANWA) - Z  軸移動至  Panel  上點位，Arm 伸出後就停止動作
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇 </param>
        /// <param name="Point"> Point 編號 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string PutWaferToStandBy(string Address, string Sequence, string Arm, string Point, string Slot)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "PutWafer", Point, Slot, Arm, "1");
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
        /// 儲存 Robot 參數
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Save(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "Save");
        }

        /// <summary>
        /// 設定 Robot 參數值 
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Type"> 參數類別(0~9) </param>
        /// <param name="No"> 參數號碼(000~999), to Kawasaki = ParameterName </param>
        /// <param name="Data"> 設定值(-99999999~+99999999), to Kawasaki = ParameterData </param>
        /// <returns></returns>
        public string setParameter(string Address, string Sequence, string Type, string No, string Data)
        {
            string CMD = Supplier == "SANWA" ? "SET" : "SET";

            string Parameter01 = string.Empty;

            if (Supplier == "SANWA" || Supplier == "ATEL_NEW")
            {
                Parameter01 = string.Format("{0},{1},{2}", Type, No, Data);
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = string.Format("{0},{1}", No, Data);
            }

            return CommandAssembly(Supplier, Address, Sequence, CMD, "Parameter", Parameter01.Split(','));
        }

        /// <summary>
        /// 設定指定點位欄位的資訊
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Teach  點位(1~1999) </param>
        /// <param name="no"> 參數號碼(000~124) </param>
        /// <param name="data"> 參數值(-99999999~+99999999) </param>
        /// <returns></returns>
        public string setSettingPointParameter(string Address, string Sequence, string pno, string no, string data)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "SetPointParameter", pno, no, data);
        }

        /// <summary>
        /// 電磁閥狀態設定 [ SANWA, ATEL ]
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
        /// 速度限制設定 [ SANWA, KAWASAKI, ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="vl"> Speed 限制，2 位數 10 進位数 </param>
        /// <returns></returns>
        public string setSpeed(string Address, string Sequence, string vl)
        {
            string Parameter01 = string.Empty;

            if (Supplier == "KAWASAKI")
            {
                Parameter01 = string.Format("{0},{1}", Address.ToString(), vl);
            }
            else
            {
                Parameter01 = vl;
            }

            return CommandAssembly(Supplier, Address, Sequence, "SET", "DeviceStatusSpeed", Parameter01.Split(','));
        }

        /// <summary>
        /// 各軸速度限制設定 [ ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="vl"> Speed 限制，2 位數 10 進位数 </param>
        /// <returns></returns>
        public string setSpeed(string Address, string Sequence, string axis, string vl)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "DeviceStatusSpeedAxis", axis, vl);
        }

        /// <summary>
        /// 各軸 Speed 限制細項設定 [ ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 讀取各個單軸設定 </param>
        /// <param name="vl"> Speed 限制，1000~99999 </param>
        /// <returns></returns>
        public string setSpeedDetail(string Address, string Sequence, string axis, string vl)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "DeviceStatusSpeedDetail", axis, vl);
        }

        /// <summary>
        /// 各個單軸馬達速度限制設定  [ ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="data"></param>
        /// <param name="axis"> 讀取各個單軸設定 </param>
        /// <param name="vl"> Speed 限制，1000~99999 </param>
        /// <returns></returns>
        public string setSpeedMotor(string Address, string Sequence, string data, string axis, string vl)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "DeviceStatusSpeedMotor", data, axis, vl);
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
            return CommandAssembly(Supplier, Address, Sequence, "SET", "DeviceStatusIO", no, vl);
        }

        /// <summary>
        /// 將目前各軸的位置寫入指定的 Point Data
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Sanwa : Teaching  點位(0001 ~ 1999)  Kawasaki (P1~P15) </param>
        /// <param name="arm"> Kawasaki 用 </param>
        /// <param name="slot"> Kawasaki 用 </param>
        /// <returns></returns>
        public string setTeachPoint(string Address, string Sequence, string pno, string arm, string slot)
        {
            string Parameter01 = string.Empty;
            string CMD = Supplier == "KAWASAKI" ?  "CMD":"SET" ;

            if (Supplier == "SANWA" || Supplier == "ATEL_NEW")
            {
                Parameter01 = pno;
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = string.Format("{0},{1}", Address.ToString(), arm, pno, slot);
            }

            return CommandAssembly(Supplier, Address, Sequence, CMD, "Teach", Parameter01.Split(','));
        }

        /// <summary>
        /// 將目前各軸的位置寫入指定的 Point Data
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Sanwa : Teaching  點位(0001 ~ 1999)  Kawasaki (P1~P15) </param>
        /// <param name="J2Data"> J2 </param>
        /// <param name="J3Data"> J3 </param>
        /// <param name="J4Data"> J4 </param>
        /// <param name="J6Data"> J6 </param>
        /// <param name="J7Data"> J7 </param>
        /// <returns></returns>
        public string setTeachPoint(string Address, string Sequence, string pno, string J2Data, string J3Data, string J4Data, string J6Data, string J7Data)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "TeachS", Address.ToString(), pno, "NULL", J2Data, J3Data, J4Data, "NULL", J6Data, J7Data);
        }

        /// <summary>
        /// 將目前各軸的位置 X Y Z 寫入指定的 Point Data
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Sanwa : Teaching  點位(0001 ~ 1999)  Kawasaki (P1~P15) </param>
        /// <param name="arm"> Kawasaki 用 </param>
        /// <param name="slot"> Kawasaki 用 </param>
        /// <returns></returns>
        public string setTeachPointXYZ(string Address, string Sequence, string pno, string XData, string YData, string ZData, string OData)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "TeachXYZ", "NULL", XData, YData, ZData, OData);
        }

        /// <summary>
        /// 取得指定點位欄位的資訊
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Teach  點位(1~1999) </param>
        /// <param name="no"> 參數號碼(000~124) </param>
        /// <returns></returns>
        public string SettingPointParameter(string Address, string Sequence, string pno, string no)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "SetPointParameter", pno, no);
        }

        /// <summary>
        /// 取得 Mapping  的厚度結果
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="no"> Data 選擇  (1~3)  </param>
        /// <returns></returns>
        public string SlotThickness(string Address, string Sequence, string no)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "SlotThickness", no);
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
        /// Speed 限制取得 [ SANWA, KAWASAKI ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Speed(string Address, string Sequence)
        {
            string Parameter01 = string.Empty;
            string Command = string.Empty;

            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    Parameter01 = null;
                    Command = "DeviceStatusSpeed";
                    break;

                case "KAWASAKI":
                    Parameter01 = Address.ToString();
                    Command =  "DeviceStatusSpeedGet";
                    break;
            }

            return CommandAssembly(Supplier, Address, Sequence, "GET", Command, Parameter01);
        }

        /// <summary>
        /// 各軸 Speed 限制細項取得 [ ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="data"> 讀取各個單軸設定 </param>
        /// <returns></returns>
        public string SpeedDetail(string Address, string Sequence, string data)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "DeviceStatusSpeedDetail", data);
        }

        /// <summary>
        /// Robot 狀態取得 [ SANWA, KAWASAKI, ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Status(string Address, string Sequence)
        {
            string Parameter01 = Supplier == "KAWASAKI" ? Address.ToString() : null;

            return CommandAssembly(Supplier, Address, Sequence, "GET", "DeviceStatus", Parameter01);
        }

        /// <summary>
        /// Robot 合併狀態取得 only Kawasaki
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
        /// 讀取  Point Data  裡的各軸位置(R~R1  六軸)
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Teach  點位  </param>
        /// <returns></returns>
        public string TeachPoint(string Address, string Sequence, string pno)
        {
            string CMD = Supplier == "SANWA" ? "GET" : "GET";
            string Command = Supplier == "KAWASAKI" ?  "TeachGet" :"Teach";

            return CommandAssembly(Supplier, Address, Sequence, CMD, Command, pno);
        }

        /// <summary>
        /// 取消 Point Data - only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Teach  點位  </param>
        /// <returns></returns>
        public string CancelTeachPoint(string Address, string Sequence, string pno)
        {
            return CommandAssembly(Supplier, Address, Sequence, "SET", "TeachCancel", pno);
        }

        /// <summary>
        /// 讀取 Point Data XYZ Coordinate - only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Teach  點位  </param>
        /// <returns></returns>
        public string TeachPointXYZ(string Address, string Sequence, string pno)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "TeachXYZGet", pno);
        }

        /// <summary>
        /// Panel(Wafer)保持  : Panel(Wafer) Hold
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="arm"> Arm 選擇  ( 1~2 ) </param>
        /// <returns></returns>
        public string WaferHold(string Address, string Sequence, string arm)
        {
            string Parameter01 = Supplier == "KAWASAKI" ?   (Address.ToString() + "," + arm):arm;

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "WaferHold", Parameter01.Split(','));
        }

        /// <summary>
        /// Panel(Wafer)  解除  : Panel(Wafer) Release
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="arm"> Arm 選擇  ( 1~2 ) </param>
        /// <returns></returns>
        public string WaferReleaseHold(string Address, string Sequence, string arm)
        {
            string Parameter01 = Supplier == "KAWASAKI" ?   (Address.ToString() + "," + arm):arm;

            return CommandAssembly(Supplier, Address, Sequence, "CMD", "WaferRelease", Parameter01.Split(','));
        }

        /// <summary>
        /// 取得  Panel(Wafer)  的狀態 
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="arm"> Sanwa 用判 Arm  選擇 </param>
        /// <param name="pno"> Kawasaki 用 需輸入點位 </param>
        /// <returns></returns>
        public string WaferStatus(string Address, string Sequence, string arm, string pno)
        {
            string CMD = Supplier == "KAWASAKI" ?   "CMD":"GET";
            string Parameter01 = string.Empty;

            if (Supplier == "SANWA" || Supplier == "ATEL_NEW")
            {
                Parameter01 = arm;
            }
            else if (Supplier == "KAWASAKI")
            {
                Parameter01 = string.Format("{0},{1}", Address.ToString(), pno);
            }

            return CommandAssembly(Supplier, Address, Sequence, CMD, "WaferStatus", Parameter01);
        }

        /// <summary>
        /// 端面檢出 : Edge Search
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Point 編號。(0001 ~ 1999) </param>
        /// <param name="arm"> Arm 選擇。  ( 1~2 ) </param>
        /// <returns></returns>
        public string EdgeSearch(string Address, string Sequence, string pno, string arm)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "EdgeSearch", pno, arm);
        }

        /// <summary>
        /// Fork 開合位置變更 : Fork
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="arm"> Arm 選擇。  ( 1~2 ) </param>
        /// <param name="fork"> 開合位置選擇  ( 0~3 ) </param>
        /// <param name="vac"> Pad 電磁閥選擇  ( 0~3 ) </param>
        /// <returns></returns>
        public string Fork(string Address, string Sequence, string arm, string fork, string vac)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "Fork", arm, fork, vac);
        }

        /// <summary>
        /// Fork Calibration
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="arm"> Arm 選擇。  ( 1~3 ) </param>
        /// <returns></returns>
        public string ForkCalibration(string Address, string Sequence, string arm)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "ForkCalibration", arm);
        }

        public string setDateTime(string Address, string Sequence, string DataTime)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "DateTime", DataTime);
        }

        public string DateTime(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "DateTimeGet", null);
        }

        /// <summary>
        /// 讀取 ROBOT ID 與版本 [ KAWASAKI ]
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Sequence"></param>
        /// <returns></returns>
        public string RobotIDAndVersion(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "RobotVersionGet", null);
        }

        /// <summary>
        /// 讀取硬體版本 [ KAWASAKI ]
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Sequence"></param>
        /// <returns></returns>
        public string RobotHardwareVersion(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "RobotHardwareVersion", null);
        }

        /// <summary>
        /// 讀取軟體版本 [ KAWASAKI, ATEL ]
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Sequence"></param>
        /// <returns></returns>
        public string RobotSoftwareVersion(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "RobotSoftwareVersion", null);
        }

        /// <summary>
        /// 讀取 Macro 狀態 [ ATEL ]
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Sequence"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        public string ReadMacroStatus(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "GET", "ReadMacroStatus");
        }

        /// <summary>
        /// 執行 Macro [ ATEL ]
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Sequence"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        public string RunMacro(string Address, string Sequence, string no)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "RunMacro", no);
        }

        /// <summary>
        /// Macro 停止 [ ATEL ]
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Sequence"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        public string StopMacro(string Address, string Sequence, string no)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "StopMacro", no);
        }


        /// <summary>
        /// Macro 結束命令 [ ATEL ]
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Sequence"></param>
        /// <returns></returns>
        public string MacroEndConnand(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "MacroEndConnand");
        }

        /// <summary>
        /// Macro 一時停止/解除一時停止 [ ATEL ]
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Sequence"></param>
        /// <param name="Active"></param>
        /// <returns></returns>
        public string PauseMacro(string Address, string Sequence, string Active)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "PauseMacro", Active);
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
                             where a.Field<string>("node_type") == "ROBOT"
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
                    case "ATEL_NEW":
                        #region SANWA

                        if (Parameter != null && Parameter.Length != 0)
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

                        if (strsParameter != null && strsParameter.Length != 0)
                        {
                            strCommandFormatParameter = container.StringFormat(dtTemp.Select(), strsParameter);
                        }

                        break;

                    #endregion

                    case "KAWASAKI":

                        #region KAWASAKI

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
                                if (dvTemp.Table.Rows[i]["Data_Value"].ToString().Equals(string.Empty))
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
                                    //if (dvTemp.Table.Rows[i]["Data_Value"].ToString().IndexOf(strsParameter[i].ToString()) < 0)
                                    //{
                                    //    throw new Exception(dvTemp.Table.Rows[i]["Data_Value"].ToString() + ": Out of setting range.");
                                    //}

                                    // * 調整文字比對方式
                                    if (!dvTemp.Table.Rows[i]["Data_Value"].ToString().Split(',').Contains(strsParameter[i].ToString()))
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

                    #endregion

                    case "ATEL":

                        #region ATEL

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
                                if (dvTemp.Table.Rows[i]["Data_Value"].ToString().Equals(string.Empty))
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
                                    //if (dvTemp.Table.Rows[i]["Data_Value"].ToString().IndexOf(strsParameter[i].ToString()) < 0)
                                    //{
                                    //    throw new Exception(dvTemp.Table.Rows[i]["Data_Value"].ToString() + ": Out of setting range.");
                                    //}

                                    if (!strsParameter[i].ToString().Equals("Empty"))
                                    {
                                        // * 調整文字比對方式
                                        if (!dvTemp.Table.Rows[i]["Data_Value"].ToString().Split(',').Contains(strsParameter[i].ToString()))
                                        {
                                            throw new Exception(dvTemp.Table.Rows[i]["Data_Value"].ToString() + ": Out of setting range.");
                                        }
                                    }
                                }

                                if (dvTemp.Table.Rows[i]["Parameter_ID"].ToString().Equals("DateTime"))
                                {
                                    strsParameter[i] = Convert.ToDateTime(strsParameter[i]).ToString("yy/MM/dd HH:mm:ss");
                                }
                            }
                        }

                        sbTemp = new StringBuilder();

                        sbTemp.Append(Address);
                        sbTemp.Append(",");

                        if (strsParameter != null)
                        { 
                            for (int i = 0; i < strsParameter.Length; i++)
                            {
                                sbTemp.Append(strsParameter[i].ToString().Equals("Empty") ? string.Empty : strsParameter[i].ToString());
                                sbTemp.Append(",");
                            }
                        }

                        strCommandFormat = container.StringFormat(dtTemp.Rows[0]["code_format"].ToString(), sbTemp.ToString().TrimEnd(',').Split(','));

                        #endregion

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
                case "ATEL":
                case "ATEL_NEW":
                    strCommand = strCommand + "\r";
                    break;

                case "KAWASAKI":
                    strCommand = strCommand + "\r\n";
                    break;
            }

            return strCommand;
        }

        public string KawasakiCheckSum(string Parameter)
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

        /// <summary>
        /// ATEL Macro Script
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="CommandType"></param>
        /// <param name="Arm"></param>
        /// <param name="Point"></param>
        /// <param name="Slot"></param>
        /// <returns></returns>
        private string GetMacroValue(string Address, string CommandType, string Arm, string Point, string Slot)
        {
            string strMsg = string.Empty;

            try
            {
                var query = (from a in dtRobotCommand.AsEnumerable()
                             where a.Field<string>("node_type") == "ROBOT"
                                && a.Field<string>("vendor") == Supplier
                                && a.Field<string>("code_type") == CommandType
                                && a.Field<string>("Parameter_ID") == Point
                                && a.Field<int>("Parameter_Order") == Convert.ToInt32(Slot)
                             select a).ToList();

                if (query.Count > 0)
                {
                    strMsg = query[0]["code_order"].ToString();
                }
                else
                {
                    throw new RowNotInTableException();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return strMsg;
        }
    }
}