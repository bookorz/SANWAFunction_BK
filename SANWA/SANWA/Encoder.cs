using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Adam.Util;

namespace SANWA.Utility
{
    public class Encoder
    {
        public EncoderAligner Aligner;
        public EncoderRobot Robot;
        public EncoderOCR OCR;
        public EncoderLoadPort LoadPort;

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
                            "WHERE vendor = @vendor";

                keyValues.Add("@vendor", Supplier);

                dtCommand = dBUtil.GetDataTable(strSql, keyValues);

                if (dtCommand.Rows.Count > 0)
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
    }
}
