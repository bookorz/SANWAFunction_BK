using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANWA.Utility
{
    interface IEncoder
    {
        // * Action

        /// <summary>
        /// 各軸移動至 HOME 位置:Normal Home
        /// </summary>
        /// <param name="Address"> Address </param>
        /// <param name="Sequence"> Sequence </param>
        /// <returns></returns>
        string Home(string Address, string Sequence);

        /// <summary>
        /// 各軸安全回 HOME 位置: Safety Home
        /// </summary>
        /// <param name="Address"> Address </param>
        /// <param name="Sequence"> Sequence </param>
        /// <returns></returns>
        string HomeSafety(string Address, string Sequence);

        string HomeOrgin(string Address, string Sequence);
        string GetWafer(string Address, string Sequence, string Arm, string Point, string Alignment, string Slot);
        string GetWaferToReady(string Address, string Sequence, string Arm, string Point, string Slot);
        string GetWaferToStandBy(string Address, string Sequence, string Arm, string Point, string Alignment, string Slot);
        string GetWaferToUp(string Address, string Sequence, string Arm, string Point, string Alignment, string Slot);
        string GetWaferToContinue(string Address, string Sequence, string Arm, string Point, string Alignment, string Slot);
        string PutWafer(string Address, string Sequence, string Arm, string Point, string Slot);
        string PutWaferToReady(string Address, string Sequence, string Arm, string Point, string Slot);
        string PutWaferToStandBy(string Address, string Sequence, string Arm, string Point, string Slot);
        string PutWaferToDown(string Address, string Sequence, string Arm, string Point, string Slot);
        string PutWaferToContinue(string Address, string Sequence, string Arm, string Point, string Slot);
        string Exchange(string Address, string Sequence, string Arm_form, string Point_form, string Slot_form, string Arm_to, string Point_to, string Slot_to);
        string WaferHold(string Address, string Sequence, string arm);
        string WaferReleaseHold(string Address, string Sequence, string arm);
        string MoveDirect(string Address, string Sequence, string axis, string type, string pos);
        string Mapping(string Address, string Sequence, string CommandType, string pno, string col, string slot);
        string Retract(string Address, string Sequence);
        string Align(string Address, string Sequence, string angle);
        string OrginSearch(string Address, string Sequence);
        string OrginSearchByAxis(string Address, string Sequence, string axis);

        // * behavior & getting
        string Status(string Address, string Sequence);
        string StatusIO(string Address, string Sequence, string nol);
        string setStatusIO(string Address, string Sequence, string no, string vl);
        string Speed(string Address, string Sequence);
        string setSpeed(string Address, string Sequence, string vl);
        string ErrorMessage(string Address, string Sequence, string no);
        string MapList(string Address, string Sequence, string no);
        string SolenoidValve(string Address, string Sequence, string no);
        string setSolenoidValve(string Address, string Sequence, string no, string vl);
        string TeachPoint(string Address, string Sequence, string pno);
        string setTeachPoint(string Address, string Sequence, string pno);
        string Parameter(string Address, string Sequence, string Type, string No);
        string setParameter(string Address, string Sequence, string Type, string No, string Data);
        string PARSY(string Address, string Sequence, string Type, string No);
        string WaferStatus(string Address, string Sequence, string arm);
        string ArmLocation(string Address, string Sequence, string Type, string Unit);
        string SlotThickness(string Address, string Sequence, string no);
        string DevicePause(string Address, string Sequence);
        string DeviceContinue(string Address, string Sequence);
        string DeviceStop(string Address, string Sequence, string m1);
        string Mode(string Address, string Sequence, string vl);
        string STPDO(string Address, string Sequence);
        string ErrorReset(string Address, string Sequence);
        string Excitation(string Address, string Sequence, string sv);
        string LogSave(string Address, string Sequence);
        string PointSave(string Address, string Sequence);
        string PointLoad(string Address, string Sequence);
        string Save(string Address, string Sequence);
        string AbssoluteEncoderReset(string Address, string Sequence, string axis);
        string AbssoluteEncoderOffset(string Address, string Sequence, string axis);
        string BatteryAlarmClear(string Address, string Sequence, string axis);
        string SettingPointParameter(string Address, string Sequence, string pno, string no);
        string setSettingPointParameter(string Address, string Sequence, string pno, string no, string data);
    }
} 
