using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANWA.Utility
{
    public class DBController : CDBContainer
    {
        public string Node_id { get; set; } //(char(10), not null)
        public string Node_function_name { get; set; } //(char(50), null)
        public string Node_function_type { get; set; } //(char(50), null)
        public string Conn_address { get; set; } //(char(20), null)
        public string Conn_type { get; set; } //(char(20), null)
        public string Conn_prot { get; set; } //(char(20), null)
        public string Com_parity_bit { get; set; } //(char(20), null)
        public string Com_data_bits { get; set; } //(char(50), not null)
        public string Com_stop_bit { get; set; } //(char(50), not null)
        public string Enable_flg { get; set; } //(char(3), not null)
        public string Create_user { get; set; } //(char(20), null)
        public DateTime Create_timestamp { get; set; } //(datetime, null)
        public string Modify_user { get; set; } //(char(20), null)
        public DateTime Modify_timestamp { get; set; } //(datetime, null)
    }
}
