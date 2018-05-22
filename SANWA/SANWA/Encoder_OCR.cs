using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace SANWA.Utility
{
    public class EncoderOCR
    {
        private string Supplier;
        private DataTable dtRobotCommand;

        /// <summary>
        /// OCR Encoder
        /// </summary>
        /// <param name="supplier"> 設備供應商 </param>
        public EncoderOCR(string supplier, DataTable dtCommand)
        {
            try
            {
                Supplier = supplier;
                dtRobotCommand = dtCommand;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public string Read(string Address, string Sequence)
        {
            return CommandAssembly(Supplier, Address, Sequence, "CMD", "Read", "-1");
        }

        private string CommandAssembly(string Supplier, string Address, string Sequence, string CommandType, string Command, params string[] Parameter)
        {
            string strCommand = string.Empty;
            string strCommandFormat = string.Empty;
            string strCommandFormatParameter = string.Empty;
            DataTable dtTemp;
            DataView dvTemp;
            ContainerSet container;
            StringBuilder sbTemp;

            try
            {

                container = new ContainerSet();
                sbTemp = new StringBuilder();

                var query = (from a in dtRobotCommand.AsEnumerable()
                             where a.Field<string>("Equipment_Type") == "ORC"
                                && a.Field<string>("Equipment_Supplier") == Supplier
                                && a.Field<string>("Command_Type") == CommandType
                                && a.Field<string>("Action_Function") == Command
                             select a).ToList();

                if (query.Count == 0)
                {
                    throw new RowNotInTableException();
                }

                dtTemp = query.CopyToDataTable();
                dtTemp.DefaultView.Sort = "Parameter_Order ASC";
                dvTemp = dtTemp.DefaultView;

                switch (Supplier)
                {
                    case "COGNEX":

                        if (Command.Equals("Read"))
                        {
                            strCommandFormat = container.StringFormat("READ({0})", Parameter);
                        }

                        break;

                    case "HST":

                        if (Command.Equals("Read"))
                        {
                            strCommandFormat = string.Format("{0}{1}{2}{3}{4}", "SM", ((char)34).ToString(), "READ", ((char)34).ToString(), "0");
                        }

                        break;
                    default:
                        throw new NotImplementedException();
                }

                strCommand = strCommandFormat + strCommandFormatParameter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return strCommand + "\r";
        }

    }
}
