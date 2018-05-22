using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace SANWA.Utility
{
    /// <summary>
    /// Alarm Message Mapping
    /// </summary>
    public class AlarmMapping
    {
        private DataTable dtCode;

        public AlarmMapping()
        {
            ContainerSet containerSet;

            try
            {
                containerSet = new ContainerSet();

                dtCode = new DataTable();

                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "Code.xml"))
                {
                    containerSet.TableFormatting(ref dtCode, System.AppDomain.CurrentDomain.BaseDirectory + "Code.xml");
                }
                else
                {
                    throw new Exception("SANWA.Utility.AlarmMapping\r\nException: Code List not exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                containerSet = null;
            }

        }

        /// <summary>
        /// Get alarm message
        /// </summary>
        /// <param name="supplier"> 設備供應商 </param>
        /// <param name="eqp_type"> 設備種類 </param>
        /// <param name="error_message"> 錯誤訊息 </param>
        /// <returns></returns>
        public AlarmMessage Get(string supplier, string eqp_type, string error_message)
        {
            AlarmMessage alarm;
            DataTable dtTemp;

            string strAlarmType = string.Empty;
            string strAlarmCode = string.Empty;
            string strAlarmAxis = string.Empty;
            string strAlarmAxisEnglish = string.Empty;
            string strCode28 = string.Empty;
            int itFirst = 0;
            int itCode28 = 0;
            string strErrorCode = string.Empty;

            try
            {
                alarm = new AlarmMessage();

                var query = (from a in dtCode.AsEnumerable()
                             where a.Field<string>("Category_ID") == "EquipmentAlarmCode"
                                && a.Field<string>("Code_Type") == string.Format("{0}.{1}", supplier.ToUpper(), eqp_type.ToUpper())
                             select a).ToList();

                dtTemp = query.CopyToDataTable();

                if (dtTemp.Rows.Count > 0)
                {
                    strAlarmType = dtTemp.Rows[0]["Code_ID"].ToString();
                    alarm.CodeID = string.Format("{0}{1}", strAlarmType, error_message);
                }
                else
                {
                    throw new Exception(string.Format("SANWA.Utility.AlarmMapping\r\nException: {0} {1} Alarm type not exists.", supplier.ToUpper(), eqp_type.ToUpper()));
                }

                // * Special rule
                switch (supplier.ToUpper())
                {
                    case "SANWA":

                        itFirst = int.Parse(error_message.Substring(0, 1));
                        strCode28 = Convert.ToString(itFirst, 2);
                        itCode28 = int.Parse(strCode28.Substring(strCode28.Length - 1, 1));

                        if (itCode28 == 1)
                        {
                            query = null;
                            query = (from a in dtCode.AsEnumerable()
                                     where a.Field<string>("Category_ID") == "AxisCode"
                                        && a.Field<string>("Code_ID") == error_message.Substring(5, 1)
                                     select a).ToList();

                            dtTemp = query.CopyToDataTable();

                            if (dtTemp.Rows.Count > 0)
                            {
                                strAlarmAxis = dtTemp.Rows[0]["Code_Cause"].ToString();
                                strAlarmAxisEnglish = dtTemp.Rows[0]["Code_Cause_English"].ToString();
                            }
                            else
                            {
                                throw new Exception("SANWA.Utility.AlarmMapping\r\nException: Alarm Axis Code not exists.");
                            }

                            strErrorCode = error_message.Substring(0, 5) + "0" + error_message.Substring(6, 2);

                        }
                        else
                        {
                            strErrorCode = error_message;
                        }

                        break;

                    case "TDK":

                        strErrorCode = error_message;

                        break;

                    default:

                        strErrorCode = error_message;

                        break;

                }


                query = null;
                query = (from a in dtCode.AsEnumerable()
                             where a.Field<string>("Category_ID") == strAlarmType
                                && a.Field<string>("Code_ID") == strErrorCode
                         select a).ToList();

                dtTemp = query.CopyToDataTable();

                if (dtTemp.Rows.Count > 0)
                {
                    alarm.Code_Type = dtTemp.Rows[0]["Code_Type"].ToString();
                    alarm.Code_Name = dtTemp.Rows[0]["Code_Name"].ToString();
                    alarm.Code_Cause = strAlarmAxis == string.Empty ? dtTemp.Rows[0]["Code_Cause"].ToString() : strAlarmAxis + " " + dtTemp.Rows[0]["Code_Cause"].ToString();
                    alarm.Code_Cause_English = strAlarmAxisEnglish == string.Empty ? dtTemp.Rows[0]["Code_Cause_English"].ToString() : strAlarmAxisEnglish + " " + dtTemp.Rows[0]["Code_Cause_English"].ToString();
                }
                else
                {
                    throw new Exception("SANWA.Utility.AlarmMapping\r\nException: Alarm Code not exists.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return alarm;
        }
    }
}
