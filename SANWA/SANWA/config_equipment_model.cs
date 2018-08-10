using log4net;
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
        private static readonly ILog logger = LogManager.GetLogger(typeof(config_equipment_model));

        public config_equipment_model()
        {
            DBUtil dBUtil;
            string strSql = string.Empty;
            List<CDBContainer> Temp = new List<CDBContainer>();
            Dictionary<string, object> keyValues = new Dictionary<string, object>();

            try
            {
                dBUtil = new DBUtil();

                strSql = "select * from config_equipment_model where equipment_model_id = @equipment_model_id";
                keyValues.Add("@equipment_model_id", SANWA.Utility.Config.SystemConfig.Get().SystemMode);

                Temp = dBUtil.GetDataList(DBUtil.QueryContainer.DBEquipmentModel, strSql, keyValues).ToList();

                if (Temp.Count == 0)
                {
                    string msg = "[NO_DATA_FOUND] Table: config_equipment_model , Equipment_model_id: " + SANWA.Utility.Config.SystemConfig.Get().SystemMode ;
                    logger.Error(msg);
                    //throw new Exception("This statement is the read config exception message.");
                    throw new Exception(msg);
                }
                else
                {
                    EquipmentModel = ((DBEquipmentModel)Temp[0]);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace,ex);
                throw new Exception(ex.ToString());
            }
        }

    }
}
