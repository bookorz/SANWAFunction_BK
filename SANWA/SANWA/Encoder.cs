using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace SANWA.Utility
{
    public class Encoder
    {
        public EncoderAligner Aligner;
        public EncoderRobot Robot;
        public EncoderOCR OCR;
        public EncoderLoadPort LoadPort;
        public Encoder_SmartTag SmartTag;

        private string Supplier;
        private DataTable dtCommand;

        private DBUtil dBUtil = new DBUtil();

        /// <summary>
        /// Encoder
        /// </summary>
        /// <param name="supplier"> Equipment supplier </param>
        public Encoder(string supplier)
        {
            ContainerSet containerSet;
            string strSql = string.Empty;
            Dictionary<string, object> keyValues = new Dictionary<string, object>();

            try
            {
                Supplier = supplier.ToUpper();

                containerSet = new ContainerSet();

                dtCommand = new DataTable();

                strSql = "SELECT * " +
                            "FROM device_code_params " +
                            "WHERE vendor = @vendor " +
                            "ORDER BY node_type, vendor, code_order";

                keyValues.Add("@vendor", Supplier);

                dtCommand = dBUtil.GetDataTable(strSql, keyValues);
                if (Supplier.Equals("SMARTTAG"))
                {
                    SmartTag = new Encoder_SmartTag(Supplier);
                }
                else if (dtCommand.Rows.Count > 0)
                {
                    Aligner = new EncoderAligner(Supplier, dtCommand);
                    Robot = new EncoderRobot(Supplier, dtCommand);
                    OCR = new EncoderOCR(Supplier, dtCommand);
                    LoadPort = new EncoderLoadPort(Supplier, dtCommand, EncoderLoadPort.CommandMode.TDK_A);
                    
                }
                else
                {
                    throw new Exception("SANWA.Utility.Encoder\r\nException: Parameter List not exists.");
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
        /// 取回命令種類
        /// </summary>
        /// <param name="command_id"> 命令ID </param>
        /// <returns></returns>
        public string GetCommandType(string command_id)
        {
            string strTemp = string.Empty;
            string strSql = string.Empty;
            DataTable dtTemp = new DataTable();

            try
            {
                var query = (from a in dtCommand.AsEnumerable()
                             where a.Field<string>("vendor") == Supplier
                                && a.Field<string>("code_id") == command_id
                             select a).ToList();

                if (query.Count == 0)
                {
                    throw new RowNotInTableException();
                }

                dtTemp = query.CopyToDataTable();

                strTemp = dtTemp.Rows[0]["code_type"].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return strTemp;
        }
    }
}
