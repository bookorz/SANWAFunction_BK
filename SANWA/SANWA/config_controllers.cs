using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANWA.Utility
{
    public class config_controllers
    {
        public DBController Aligner01;
        public DBController Aligner02;
        public DBController Robot01;
        public DBController Robot02;
        public DBController OCR01;
        public DBController OCR02;
        public DBController LoadPort01;
        public DBController LoadPort02;
        public DBController LoadPort03;
        public DBController LoadPort04;
        public DBController LoadPort05;
        public DBController LoadPort06;
        public DBController LoadPort07;
        public DBController LoadPort08;

        public config_controllers()
        {
            DBUtil dBUtil;
            string strSql = string.Empty;
            List<CDBContainer> Temp = new List<CDBContainer>();

            try
            {
                dBUtil = new DBUtil();

                strSql = "select * from config_controller order by node_id asc ";

                Temp = dBUtil.GetDataList(DBUtil.QueryContainer.DBController, strSql, null).ToList();

                if (Temp.Count == 0)
                {
                    throw new Exception("This statement is the read config exception message.");
                }

                foreach (var item in Temp)
                {
                    switch (((DBController)item).Node_id.ToString())
                    {
                        case "Aligner01":
                            Aligner01 = ((DBController)item);
                            break;

                        case "Aligner02":
                            Aligner02 = ((DBController)item);
                            break;

                        case "LoadPort01":
                            LoadPort01 = ((DBController)item);
                            break;

                        case "LoadPort02":
                            LoadPort02 = ((DBController)item);
                            break;

                        case "LoadPort03":
                            LoadPort03 = ((DBController)item);
                            break;

                        case "LoadPort04":
                            LoadPort04 = ((DBController)item);
                            break;

                        case "LoadPort05":
                            LoadPort05 = ((DBController)item);
                            break;

                        case "LoadPort06":
                            LoadPort06 = ((DBController)item);
                            break;

                        case "LoadPort07":
                            LoadPort07 = ((DBController)item);
                            break;

                        case "LoadPort08":
                            LoadPort08 = ((DBController)item);
                            break;

                        case "Robot01":
                            Robot01 = ((DBController)item);
                            break;

                        case "Robot02":
                            Robot02 = ((DBController)item);
                            break;

                        case "OCR01":
                            OCR01 = ((DBController)item);
                            break;

                        case "OCR02":
                            OCR02 = ((DBController)item);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}
