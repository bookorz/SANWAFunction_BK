using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using log4net;

namespace SANWA.Utility
{
    /// <summary>
    /// Alarm Message Mapping
    /// </summary>
    public class AlarmMapping
    {
        private DataTable dtCode;
        private DataTable dtAlarmEvent;
        private static readonly ILog logger = LogManager.GetLogger(typeof(AlarmMapping));

        public AlarmMapping()
        {
            ContainerSet containerSet;
            string strSql = string.Empty;
            DBUtil dBUtil = new DBUtil();
            try
            {
                containerSet = new ContainerSet();

                dtCode = new DataTable();

                strSql = "SELECT * FROM view_alarm_code order by alarm_code_id asc";

                dtCode = dBUtil.GetDataTable(strSql, null);

                if (dtCode.Rows.Count < 0)
                {
                    throw new Exception("SANWA.Utility.AlarmMapping\r\nException: Code List not exists.");
                }

                strSql = "SELECT * FROM alarm_event_config WHERE active = 'Y'";

                dtAlarmEvent = dBUtil.GetDataTable(strSql, null);

            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace, ex);
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
            string strSql = string.Empty;
            DBUtil dBUtil = new DBUtil();

            try
            {
                alarm = new AlarmMessage();

                // * Special rule
                switch (supplier.ToUpper())
                {
                    case "SANWA":

                        itFirst = int.Parse(error_message.Substring(0, 1));
                        strCode28 = Convert.ToString(itFirst, 2);
                        itCode28 = int.Parse(strCode28.Substring(strCode28.Length - 1, 1));

                        if (itCode28 == 1)
                        {
                            strSql = "SELECT * " +
                                        "FROM config_list_item " +
                                        "WHERE list_type = 'SANWA_CODE' " +
                                        "ORDER BY sort_sequence ASC";

                            dtTemp = dBUtil.GetDataTable(strSql, null);

                            if (dtTemp.Rows.Count > 0)
                            {
                                strAlarmAxis = dtTemp.Rows[0]["list_name"].ToString();
                                strAlarmAxisEnglish = dtTemp.Rows[0]["list_name_en"].ToString();
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

                    case "KAWASAKI":

                        strErrorCode = error_message;

                        break;

                    default:

                        strErrorCode = error_message;

                        break;

                }

                var query = (from a in dtCode.AsEnumerable()
                             where a.Field<string>("node_type") == eqp_type.ToUpper()
                                && a.Field<string>("vendor") == supplier.ToUpper()
                                && a.Field<string>("Code_ID") == strErrorCode.ToUpper()
                             select a).ToList();

                if (query.Count > 0)
                {
                    dtTemp = query.CopyToDataTable();
                    strAlarmType = dtTemp.Rows[0]["alarm_code_id"].ToString();
                    strAlarmCode = dtTemp.Rows[0]["Code_ID"].ToString();
                    alarm.CodeID = string.Format("{0}{1}", strAlarmType, strAlarmCode);
                    alarm.IsStop = dtTemp.Rows[0]["Is_stop"].ToString() == "Y" ? true : false;
                }
                else
                {
                    throw new Exception(string.Format("SANWA.Utility.AlarmMapping\r\nException: {0} {1} Alarm type not exists.", supplier.ToUpper(), eqp_type.ToUpper()));
                }


                query = null;
                query = (from a in dtCode.AsEnumerable()
                         where a.Field<string>("node_type") == eqp_type.ToUpper()
                            && a.Field<string>("vendor") == supplier.ToUpper()
                            && a.Field<string>("alarm_code_id") == strAlarmType
                            && a.Field<string>("Code_ID") == strAlarmCode
                         select a).ToList();

                dtTemp = query.CopyToDataTable();

                if (query.Count > 0)
                {
                    dtTemp = query.CopyToDataTable();
                    alarm.Code_Type = dtTemp.Rows[0]["Code_Type"].ToString();
                    alarm.Code_Name = dtTemp.Rows[0]["Code_Name"].ToString();
                    alarm.Code_Cause = strAlarmAxis == string.Empty ? dtTemp.Rows[0]["Code_Desc"].ToString() : strAlarmAxis + " " + dtTemp.Rows[0]["Code_Desc"].ToString();
                    alarm.Code_Cause_English = strAlarmAxisEnglish == string.Empty ? dtTemp.Rows[0]["Code_Desc_EN"].ToString() : strAlarmAxisEnglish + " " + dtTemp.Rows[0]["Code_Desc_EN"].ToString();
                }
                else
                {
                    throw new Exception("SANWA.Utility.AlarmMapping\r\nException: Alarm Code not exists.");
                }

                query = (from a in dtAlarmEvent.AsEnumerable()
                        where a.Field<string>("Device_Name") == supplier.ToUpper()
                           && a.Field<string>("alarm_no") == strAlarmCode
                         select a).ToList();

                if (query.Count > 0)
                {
                    dtTemp = query.CopyToDataTable();
                }
                else
                {
                    dtTemp = new DataTable();
                }

                if (dtTemp.Rows.Count > 0)
                {
                    alarm.LED_Red = dtTemp.Rows[0]["led_red"].ToString();
                    alarm.LED_Yellow = dtTemp.Rows[0]["led_yellow"].ToString();
                    alarm.LED_Green = dtTemp.Rows[0]["led_green"].ToString();
                    alarm.LED_Bule = dtTemp.Rows[0]["led_bule"].ToString();
                    alarm.Buzzer01 = dtTemp.Rows[0]["buzzer_01"].ToString();
                    alarm.Buzzer02 = dtTemp.Rows[0]["buzzer_02"].ToString();
                    alarm.IsStop = dtTemp.Rows[0]["Is_stop"].ToString() == "Y" ? true : false;
                }
                else
                {
                    alarm.LED_Red = "N";
                    alarm.LED_Yellow = "N";
                    alarm.LED_Green = "N";
                    alarm.LED_Bule = "N";
                    alarm.Buzzer01 = "N";
                    alarm.Buzzer02 = "N";
                    alarm.IsStop = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return alarm;
        }


        /// <summary>
        /// Get alarm message only ATEL
        /// </summary>
        /// <param name="eqp_type"> 設備種類 </param>
        /// <param name="Command"> 輸入命令 </param>
        /// <param name="error_message"> 錯誤訊息 </param>
        /// <returns></returns>
        public AlarmMessage GetATEL(string eqp_type, string Command, string error_message)
        {
            AlarmMessage alarm;
            DataTable dtTemp;
            string supplier = "ATEL";
            string strAlarmType = string.Empty;
            string strAlarmCode = string.Empty;
            string strAlarmAxis = string.Empty;
            string strAlarmAxisEnglish = string.Empty;
            string strSql = string.Empty;
            DBUtil dBUtil = new DBUtil();

            try
            {
                alarm = new AlarmMessage();

                var query = (from a in dtCode.AsEnumerable()
                             where a.Field<string>("node_type") == eqp_type.ToUpper()
                                && a.Field<string>("vendor") == supplier.ToUpper()
                                && Command.Contains(a.Field<string>("code_type"))
                                && a.Field<string>("code_id") == error_message.ToUpper()
                             select a).ToList();

                if (query.Count > 0)
                {
                    dtTemp = query.CopyToDataTable();
                    strAlarmType = dtTemp.Rows[0]["alarm_code_id"].ToString();
                    strAlarmCode = dtTemp.Rows[0]["Code_ID"].ToString();
                    alarm.CodeID = string.Format("{0}{1}", strAlarmType, strAlarmCode);
                    alarm.IsStop = dtTemp.Rows[0]["Is_stop"].ToString() == "Y" ? true : false;

                    alarm.Code_Type = dtTemp.Rows[0]["Code_Type"].ToString();
                    alarm.Code_Name = dtTemp.Rows[0]["Code_Name"].ToString();
                    alarm.Code_Cause = strAlarmAxis == string.Empty ? dtTemp.Rows[0]["Code_Desc"].ToString() : strAlarmAxis + " " + dtTemp.Rows[0]["Code_Desc"].ToString();
                    alarm.Code_Cause_English = strAlarmAxisEnglish == string.Empty ? dtTemp.Rows[0]["Code_Desc_EN"].ToString() : strAlarmAxisEnglish + " " + dtTemp.Rows[0]["Code_Desc_EN"].ToString();
                }
                else
                {
                    throw new Exception(string.Format("SANWA.Utility.AlarmMapping\r\nException: {0} {1} Alarm type not exists.", supplier.ToUpper(), eqp_type.ToUpper()));
                }

                query = (from a in dtAlarmEvent.AsEnumerable()
                         where a.Field<string>("Device_Name") == supplier.ToUpper()
                            && a.Field<string>("alarm_no") == strAlarmCode
                         select a).ToList();

                if (query.Count > 0)
                {
                    dtTemp = query.CopyToDataTable();
                }
                else
                {
                    dtTemp = new DataTable();
                }

                if (dtTemp.Rows.Count > 0)
                {
                    alarm.LED_Red = dtTemp.Rows[0]["led_red"].ToString();
                    alarm.LED_Yellow = dtTemp.Rows[0]["led_yellow"].ToString();
                    alarm.LED_Green = dtTemp.Rows[0]["led_green"].ToString();
                    alarm.LED_Bule = dtTemp.Rows[0]["led_bule"].ToString();
                    alarm.Buzzer01 = dtTemp.Rows[0]["buzzer_01"].ToString();
                    alarm.Buzzer02 = dtTemp.Rows[0]["buzzer_02"].ToString();
                    alarm.IsStop = dtTemp.Rows[0]["Is_stop"].ToString() == "Y" ? true : false;
                }
                else
                {
                    alarm.LED_Red = "N";
                    alarm.LED_Yellow = "N";
                    alarm.LED_Green = "N";
                    alarm.LED_Bule = "N";
                    alarm.Buzzer01 = "N";
                    alarm.Buzzer02 = "N";
                    alarm.IsStop = false;
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
