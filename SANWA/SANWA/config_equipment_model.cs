using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANWA.Utility
{
    public class config_equipment_model
    {
        public DBEquipmentModel EquipmentModel;

        public config_equipment_model()
        {
            Adam.Util.DBUtil dBUtil;
            string strSql = string.Empty;
            List<CDBContainer> Temp = new List<CDBContainer>();
            Dictionary<string, object> keyValues = new Dictionary<string, object>();

            try
            {
                dBUtil = new Adam.Util.DBUtil();

                strSql = "select * from config_equipment_model where equipment_model_id = @equipment_model_id";
                keyValues.Add("@equipment_model_id", SANWA.Utility.Config.SystemConfig.Get().SystemMode);

                Temp = dBUtil.GetDataList(Adam.Util.DBUtil.QueryContainer.DBEquipmentModel, strSql, keyValues).ToList();

                if (Temp.Count == 0)
                {
                    throw new Exception("This statement is the read config exception message.");
                }

                EquipmentModel = ((DBEquipmentModel)Temp[0]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}
