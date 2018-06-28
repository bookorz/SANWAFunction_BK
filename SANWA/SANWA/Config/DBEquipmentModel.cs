using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANWA.Utility
{
    public class DBEquipmentModel : CDBContainer
    {
        public string equipment_model_id { get; set; } //(char(20), not null)
        public string equipment_model_type { get; set; } //(char(100), not null)
        public string enable_flg { get; set; } //(char(3), null)
        public string create_user { get; set; } //(char(20), null)
        public DateTime create_timestamp { get; set; } //(datetime, not null)
        public string modify_user { get; set; } //(char(20), null)
        public DateTime modify_timestamp { get; set; } //(datetime, not null)
    }
}
