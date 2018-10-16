using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace SANWA.Utility
{
    public class EncoderLoadPort
    {
        private string Supplier;
        private DataTable dtRobotCommand;
        private CommandMode cmdMode;

        /// <summary>
        /// Indicator Type
        /// </summary>
        public enum IndicatorType
        {
            Load,
            Unload,
            OpAccess,
            Presence,
            Placement,
            Status01,
            Status02
        }

        /// <summary>
        /// Indicator Status
        /// </summary>
        public enum IndicatorStatus
        {
            ON,
            OFF,
            Flashes
        }

        /// <summary>
        /// Load Port Runing Mode [TDK, ASYST]
        /// </summary>
        public enum ModeType
        {
            Online,
            Maintenance,
            Auto,
            Manual
        }

        /// <summary>
        /// Load Port Parameter state [TDK, ASYST]
        /// </summary>
        public enum ParamState
        {
            Enable,
            Disable
        }

        /// <summary>
        /// Wafer Sorting Type
        /// </summary>
        public enum MappingSortingType
        {
            Asc,
            Desc
        }

        /// <summary>
        /// Command Type [TDK, ASYST]
        /// </summary>
        public enum CommandType
        {
            Normal,
            Finish
        }

        public enum EventType
        {
            Complete
        }

        /// <summary>
        /// Command Type
        /// </summary>
        public enum CommandMode
        {
            TDK_A,
            TDK_B
        }

        /// <summary>
        /// Load Port Encoder
        /// </summary>
        /// <param name="supplier"> 設備供應商 </param>
        public EncoderLoadPort(string supplier, DataTable dtCommand, CommandMode commandMode)
        {
            try
            {
                Supplier = supplier;
                dtRobotCommand = dtCommand;
                cmdMode = commandMode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Reset [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Reset(CommandType commandType)
        {
            string Command = string.Empty;

            switch (commandType)
            {
                case CommandType.Normal:
                    switch (Supplier)
                    {
                        case "TDK":
                            Command = "SET";
                            break;

                        case "ASYST":
                            Command = "HCS";
                            break;
                    }
                    break;

                case CommandType.Finish:
                    Command = "FIN";
                    break;
            }

            return CommandAssembly(Supplier, Command, "Reset");
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Initialization(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "SET", "Initialization");
        }

        /// <summary>
        /// Move to slot number
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <param name="Slot">Move to slot number</param>
        /// <returns></returns>
        public string Slot(CommandType commandType, string Slot)
        {
            string Command = string.Empty;
            try
            {
                switch (commandType)
                {
                    case CommandType.Normal:
                        switch (Supplier)
                        {
                            case "ASYST":
                                Command = "HCS";
                                break;
                        }
                        break;

                }
                Slot = Convert.ToInt32(Slot).ToString();//Remove 0
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return CommandAssembly(Supplier, Command, "Slot", new string[] { Slot });
        }

        public string SetEvent(CommandType commandType, EventType evtType, ParamState state)
        {
            string Command = string.Empty;
            string parm = string.Empty;
            try
            {
                switch (Supplier)
                {
                    case "ASYST":
                        switch (evtType)
                        {
                            case EventType.Complete:

                                Command = "EDER";
                                if(state == ParamState.Enable)
                                {
                                    parm = "ON";
                                }
                                else if(state == ParamState.Disable)
                                {
                                    parm = "OFF";
                                }
                                break;
                        }
                        break;

                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return CommandAssembly(Supplier, Command, Command+"_"+ parm);
        }

        /// <summary>
        /// Status Indicator
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <param name="indicatorType"> Indicator Type </param>
        /// <param name="indicatorStatus"> Indicator Status</param>
        /// <returns></returns>
        public string Indicator(CommandType commandType, IndicatorType indicatorType, IndicatorStatus indicatorStatus)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "SET", string.Format("{0}_{1}", indicatorType.ToString(), indicatorStatus.ToString()));
        }

        /// <summary>
        /// Load Port Runing Mode [TDK, ASYST]
        /// </summary>
        /// <param name="modeType"> Runing Mode Type </param>
        /// <returns></returns>
        public string Mode(ModeType modeType)
        {
            string Command = string.Empty;

            switch (modeType)
            {
                case ModeType.Online:
                case ModeType.Maintenance:
                    Command = "MOD";
                    break;

                case ModeType.Auto:
                case ModeType.Manual:
                    Command = "HCS";
                    break;
            }

            return CommandAssembly(Supplier, Command, modeType.ToString());
        }

        /// <summary>
        /// Status [TDK, ASYST]
        /// </summary>
        /// <returns></returns>
        public string Status()
        {
            string Command = string.Empty;

            switch (Supplier)
            {
                case "ASYST":
                    Command = CommandAssembly(Supplier, "FSR", "Status");
                    break;

                case "TDK":
                    Command = CommandAssembly(Supplier, "GET", "Status");
                    break;
            }

            return Command;
        }

        /// <summary>
        /// Version
        /// </summary>
        /// <returns></returns>
        public string Version()
        {
            return CommandAssembly(Supplier, "GET", "Version");
        }

        /// <summary>
        /// LED Indicator Status
        /// </summary>
        /// <returns></returns>
        public string LEDIndicatorStatus()
        {
            return CommandAssembly(Supplier, "GET", "LEDStatus");
        }

        /// <summary>
        /// Wafer Sorting [TDK, ASYST]
        /// </summary>
        /// <param name="sortingType"> Sorting Type</param>
        /// <returns></returns>
        public string WaferSorting(MappingSortingType sortingType)
        {
            string Command = string.Empty;

            switch (Supplier)
            {
                case "ASYST":
                    Command = CommandAssembly(Supplier, "FSR", "WaferSorting");
                    break;

                case "TDK":
                    Command = CommandAssembly(Supplier, "GET", string.Format("WaferSorting_{0}", sortingType.ToString()));
                    break;
            }

            return Command;
        }

        /// <summary>
        /// Wafer Quantity
        /// </summary>
        /// <returns></returns>
        public string WaferQuantity()
        {
            return CommandAssembly(Supplier, "GET", "WaferQuantity");
        }

        /// <summary>
        /// FOUP Initial Position [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string InitialPosition(CommandType commandType)
        {
            string Command = string.Empty;

            switch (commandType)
            {
                case CommandType.Normal:
                    switch (Supplier)
                    {
                        case "TDK":
                            Command = "MOV";
                            break;

                        case "ASYST":
                            Command = "HCS";
                            break;
                    }
                    break;

                case CommandType.Finish:
                    Command = "FIN";
                    break;
            }

            return CommandAssembly(Supplier, Command, "InitialPosition");
        }

        /// <summary>
        /// FOUP Forced Initial Position
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string ForcedInitialPosition(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "ForcedInitialPosition");
        }

        /// <summary>
        /// Load FOUP [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Load(CommandType commandType)
        {
            string Command = string.Empty;

            switch (commandType)
            {
                case CommandType.Normal:
                    switch (Supplier)
                    {
                        case "TDK":
                            Command = "MOV";
                            break;

                        case "ASYST":
                            Command = "HCS";
                            break;
                    }
                    break;

                case CommandType.Finish:
                    Command = "FIN";
                    break;
            }

            return CommandAssembly(Supplier, Command, "Load");
        }

        /// <summary>
        /// Docking Position
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DockingPosition(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "DockingPosition");
        }

        /// <summary>
        /// Docking Position (No Vac)
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DockingPositionNoVac(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "DockingPositionNoVac");
        }

        /// <summary>
        /// Docking Position After Load
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DockingPositionAfterLoad(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "DockingPositionAfterLoad");
        }

        /// <summary>
        /// Mapping Load  [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MappingLoad(CommandType commandType)
        {
            string Command = string.Empty;

            switch (commandType)
            {
                case CommandType.Normal:
                    switch (Supplier)
                    {
                        case "TDK":
                            Command = "MOV";
                            break;

                        case "ASYST":
                            Command = "HCS";
                            break;
                    }
                    break;

                case CommandType.Finish:
                    Command = "FIN";
                    break;
            }

            return CommandAssembly(Supplier, Command, "MappingLoad");
        }

        /// <summary>
        /// Docking Position After Mapping
        /// </summary>
        ///         /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DockingPositionAfterMapping(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "DockingPositionAfterMapping");
        }

        /// <summary>
        /// Unload FOUP [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Unload(CommandType commandType)
        {
            string Command = string.Empty;

            switch (commandType)
            {
                case CommandType.Normal:
                    switch (Supplier)
                    {
                        case "TDK":
                            Command = "MOV";
                            break;

                        case "ASYST":
                            Command = "HCS";
                            break;
                    }
                    break;

                case CommandType.Finish:
                    Command = "FIN";
                    break;
            }

            return CommandAssembly(Supplier, Command, "Unload");
        }

        /// <summary>
        /// Until Door Close
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string UntilDoorClose(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "UntilDoorClose");
        }

        /// <summary>
        /// Door Close After Undock
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DoorCloseAfterUndock(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "DoorCloseAfterUndock");
        }

        /// <summary>
        /// Door Close After Unload
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DoorCloseAfterUnload(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "DoorCloseAfterUnload");
        }

        /// <summary>
        ///  Until Door Close Vac OFF
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string UntilDoorCloseVacOFF(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "UntilDoorCloseVacOFF");
        }

        /// <summary>
        ///  Until Undock
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string UntilUndock(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "UntilUndock");
        }

        /// <summary>
        ///  Map & Unload
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapAndUnload(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "MapAndUnload");
        }

        /// <summary>
        ///  Map & until door close
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapAndUntilDoorClose(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "MapAndUntilDoorClose");
        }

        /// <summary>
        ///  Map & until undock
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapAndUntilUndock(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "MapAndUntilUndock");
        }

        /// <summary>
        ///  Mapping in Load
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MappingInLoad(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "MappingInLoad");
        }

        /// <summary>
        ///  Retry Mapping
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string RetryMapping(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "RetryMapping");
        }

        /// <summary>
        ///  FOUP Clamp Release [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string FOUPClampRelease(CommandType commandType)
        {
            string Command = string.Empty;

            switch (commandType)
            {
                case CommandType.Normal:
                    switch (Supplier)
                    {
                        case "TDK":
                            Command = "MOV";
                            break;

                        case "ASYST":
                            Command = "HCS";
                            break;
                    }
                    break;

                case CommandType.Finish:
                    Command = "FIN";
                    break;
            }

            return CommandAssembly(Supplier, Command, "FOUPClampRelease");
        }

        /// <summary>
        ///  FOUP Clamp Fix [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string FOUPClampFix(CommandType commandType)
        {
            string Command = string.Empty;

            switch (commandType)
            {
                case CommandType.Normal:
                    switch (Supplier)
                    {
                        case "TDK":
                            Command = "MOV";
                            break;

                        case "ASYST":
                            Command = "HCS";
                            break;
                    }
                    break;

                case CommandType.Finish:
                    Command = "FIN";
                    break;
            }

            return CommandAssembly(Supplier, Command, "FOUPClampFix");
        }

        /// <summary>
        ///  Load Port Undock
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Undock(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "Undock");
        }

        /// <summary>
        ///  Load Port Dock
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Dock(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "Dock");
        }

        /// <summary>
        ///  Load Port Vacuum OFF
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string VacuumOFF(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "VacuumOFF");
        }

        /// <summary>
        ///  Load Port Vacuum ON
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string VacuumON(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "VacuumON");
        }

        /// <summary>
        ///  Latch key Fix
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string LatchkeyFix(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "LatchkeyFix");
        }

        /// <summary>
        ///  Latch key Release
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string LatchkeyRelease(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "LatchkeyRelease");
        }

        /// <summary>
        ///  Door Close
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DoorClose(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "DoorClose");
        }

        /// <summary>
        ///  Door Open
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DoorOpen(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "DoorOpen");
        }

        /// <summary>
        ///  Door Up
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DoorUp(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "DoorUp");
        }

        /// <summary>
        ///  Door Down
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DoorDown(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "DoorDown");
        }

        /// <summary>
        ///  Mapper Stopper ON
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapperStopperON(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "(MapperStopperON");
        }

        /// <summary>
        ///  Mapper Stopper OFF
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapperStopperOFF(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "MapperStopperOFF");
        }

        /// <summary>
        ///  Mapper Wait Position
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapperWaitPosition(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "MapperWaitPosition");
        }

        /// <summary>
        ///  Mapper Start Position
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapperStartPosition(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "MapperStartPosition");
        }

        /// <summary>
        ///  Mapper Arm Close
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapperArmClose(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "MapperArmClose");
        }

        /// <summary>
        ///  Mapper Arm Open
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapperArmOpen(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "MapperArmOpen");
        }

        /// <summary>
        ///  Mapping Down
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MappingDown(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "MappingDown");
        }

        /// <summary>
        ///  Retry
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Retry(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "Retry");
        }

        /// <summary>
        ///  Stop
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Stop(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "Stop");
        }

        /// <summary>
        ///  Pause
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Pause(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "Pause");
        }

        /// <summary>
        ///  Abort [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Abort(CommandType commandType)
        {
            string Command = string.Empty;

            switch (commandType)
            {
                case CommandType.Normal:
                    switch (Supplier)
                    {
                        case "TDK":
                            Command = "MOV";
                            break;

                        case "ASYST":
                            Command = "HCS";
                            break;
                    }
                    break;

                case CommandType.Finish:
                    Command = "FIN";
                    break;
            }

            return CommandAssembly(Supplier, Command, "Abort");
        }

        /// <summary>
        /// Load Port Advancing box sensing pads sensor [TDK, ASYST]
        /// </summary>
        /// <param name="modeType"> Runing Mode Type </param>
        /// <returns></returns>
        public string EQASP(ParamState state)
        {
            string Command = string.Empty;

            Command = "PRM";

            return CommandAssembly(Supplier, Command, string.Format("EQASP_{0}", state.ToString()));
        }

        /// <summary>
        ///  Resum
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Resum(CommandType commandType)
        {
            return CommandAssembly(Supplier, commandType.ToString().Equals("Finish") ? "FIN" : "MOV", "Resum");
        }

        private string CommandAssembly(string Supplier, string CommandType, string Command, params string[] Parameter)
        {
            string strCommand = string.Empty;
            string strCommandFormat = string.Empty;
            string strCommandFormatParameter = string.Empty;
            DataTable dtTemp;
            DataView dvTemp;
            ContainerSet container;
            StringBuilder sbTemp;

            try
            {

                container = new ContainerSet();
                sbTemp = new StringBuilder();

                var query = (from a in dtRobotCommand.AsEnumerable()
                             where a.Field<string>("node_type") == "LOADPORT"
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
                    case "TDK":

                        switch (cmdMode)
                        {
                            case CommandMode.TDK_A:
                                strCommandFormat = TDK_A(dtTemp.Rows[0]["code_format"].ToString());
                                break;
                            case CommandMode.TDK_B:
                                strCommandFormat = TDK_B(dtTemp.Rows[0]["code_format"].ToString());
                                break;
                        }

                        break;

                    case "ASYST":

                        strCommandFormat = dtTemp.Rows[0]["code_format"].ToString();
                        strCommandFormatParameter = string.Empty;

                        foreach (string str in Parameter)
                        {
                            strCommandFormatParameter = strCommandFormatParameter + " " + str;
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
                case "ASYST":

                    strCommand = strCommand + "\r\n";

                    break;
            }

            return strCommand;
        }

        public string TDK_A(string Command)
        {
            string strCommsnd = string.Empty;
            string strLen = string.Empty;
            string sCheckSum = string.Empty;
            int chrLH = 0;
            int chrLL = 0;

            try
            {
                strLen = Convert.ToString(Command.Length + 4, 16).PadLeft(2, '0');

                chrLH = 0;
                chrLL = Convert.ToInt32(strLen, 16);
                strLen = Convert.ToChar(chrLH).ToString() + Convert.ToChar(chrLL).ToString();
                sCheckSum = ProcCheckSum(strLen, Command);
                strCommsnd = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", Convert.ToChar(1), strLen, Convert.ToChar(48), string.Empty, Convert.ToChar(48), Command, sCheckSum, Convert.ToChar(3));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return strCommsnd;
        }

        public string TDK_B(string Command)
        {
            string strCommsnd = string.Empty;
            string strLen = string.Empty;

            try
            {
                strLen = Convert.ToString(Command.Length + 4, 16).PadLeft(2, '0');
                strCommsnd = string.Format("{0}{1}{2}{3}", Convert.ToChar("s"), strLen, Command, Convert.ToChar(13));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return strCommsnd;
        }

        public string ProcCheckSum(string Len, string Message)
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
