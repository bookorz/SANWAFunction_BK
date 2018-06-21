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

        public enum OnlineStatus
        {
            Offline,
            Online
        }

        public string Read()
        {
            return CommandAssembly("CMD", "Read", "-1");
        }

        public string SetOnline(OnlineStatus online)
        {
            return CommandAssembly("CMD", "OnlineStatus", ((int)online).ToString());
        }

        public string GetOnline()
        {
            return CommandAssembly("CMD", "GetOnline");
        }

        private string CommandAssembly(string CommandType, string Command, params string[] Parameter)
        {
            string strCommand = string.Empty;
            string strCommandFormat = string.Empty;
            string strCommandFormatParameter = string.Empty;
            ContainerSet container;
            StringBuilder sbTemp;

            try
            {

                container = new ContainerSet();
                sbTemp = new StringBuilder();

                //var query = (from a in dtRobotCommand.AsEnumerable()
                //             where a.Field<string>("Equipment_Type") == "ORC"
                //                && a.Field<string>("Equipment_Supplier") == Supplier
                //                && a.Field<string>("Command_Type") == CommandType
                //                && a.Field<string>("Action_Function") == Command
                //             select a).ToList();

                //if (query.Count == 0)
                //{
                //    throw new RowNotInTableException();
                //}

                //dtTemp = query.CopyToDataTable();
                //dtTemp.DefaultView.Sort = "Parameter_Order ASC";
                //dvTemp = dtTemp.DefaultView;

                switch (Supplier)
                {
                    case "COGNEX":

                        if (Command.Equals("Read"))
                        {
                            strCommandFormat = container.StringFormat("READ({0})", Parameter) + Environment.NewLine;
                        }
                        else
                        {
                            throw new DriveNotFoundException();
                        }

                        break;

                    case "HST":

                        if (Command.Equals("Read"))
                        {
                            strCommandFormat = string.Format("{0}{1}{2}{3}{4}", "SM", ((char)34).ToString(), "READ", ((char)34).ToString(), "0");
                        }
                        else if (Command.Equals("OnlineStatus"))
                        {
                            strCommandFormat = string.Format("SO{0}", Parameter);
                        }
                        else if (Command.Equals("GetOnline"))
                        {
                            strCommandFormat = "GO";
                        }
                        else
                        {
                            throw new DriveNotFoundException();
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

            return strCommand;
        }

        private string COGNEXImageStore()
        {
            FTP fTP = null;

            string strIpAddress = string.Empty;
            string strLoginUser = string.Empty;
            string strFtpPort = string.Empty;
            string strSavePath = string.Empty;
            string strFileName = string.Empty;
            string strRemoteFileName = string.Empty;
            string strLocalFilePath = string.Empty;

            try
            {
                strIpAddress = "192.168.0.5";
                strLoginUser = "admin";
                strFtpPort = "21";
                strSavePath = System.AppDomain.CurrentDomain.BaseDirectory + "CognexReadImage\\";
                strFileName = DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + ".bmp";
                strRemoteFileName = "Image.bmp";

                fTP = new FTP(strIpAddress, strFtpPort, string.Empty, strLoginUser, string.Empty);
                strLocalFilePath = fTP.Get(strRemoteFileName, strFileName, strSavePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return strLocalFilePath;
        }

        private string HSTImageStore()
        {

            string strSavePath = string.Empty;
            string strFileName = string.Empty;
            string strLocalFilePath = string.Empty;

            try
            {
                strSavePath = System.AppDomain.CurrentDomain.BaseDirectory + "HSTReadImage\\";
                strFileName = DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + ".bmp";
                string lastCreateFileName = String.Empty;
                DateTime lastCreateFileTime = DateTime.MinValue;

                DirectoryInfo dirInfo = new DirectoryInfo("C:\\新資料夾");
                foreach (FileInfo fileInfo in dirInfo.GetFiles())
                {
                    if (fileInfo.CreationTime > lastCreateFileTime)
                    {
                        lastCreateFileTime = fileInfo.CreationTime;
                        lastCreateFileName = fileInfo.Name;
                    }
                }

                System.IO.File.Copy(lastCreateFileName, System.IO.Path.Combine(strSavePath, strFileName), true);
                strLocalFilePath = System.IO.Path.Combine(strSavePath, strFileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return strLocalFilePath;
        }
    }
}
