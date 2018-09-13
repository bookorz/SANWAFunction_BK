using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANWA.Utility
{
    public class AlarmMessage
    {
        public string CodeID = string.Empty;
        public string Return_Code_ID = string.Empty;
        public string Code_Type = string.Empty;
        public string Code_Name = string.Empty;
        public string Code_Cause = string.Empty;
        public string Code_Cause_English = string.Empty;
        //public string LED_Red = string.Empty;
        //public string LED_Yellow = string.Empty;
        //public string LED_Green = string.Empty;
        //public string LED_Bule = string.Empty;
        //public string Buzzer01 = string.Empty;
        //public string Buzzer02 = string.Empty;
        public bool IsStop = false;
    }
}
