using System;
using System.Data;
using System.IO;

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

        /// <summary>
        /// Encoder
        /// </summary>
        /// <param name="supplier"> Equipment supplier </param>
        public Encoder(string supplier)
        {
            ContainerSet containerSet;

            try
            {
                Supplier = supplier.ToUpper();

                containerSet = new ContainerSet();

                dtCommand = new DataTable();

                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "ParameterList.xml"))
                {
                    containerSet.TableFormatting(ref dtCommand, System.AppDomain.CurrentDomain.BaseDirectory + "ParameterList.xml");
                }
                else
                {
                    throw new Exception("SANWA.Utility.Encoder\r\nException: Parameter List not exists.");
                }

                Aligner = new EncoderAligner(Supplier, dtCommand);
                Robot = new EncoderRobot(Supplier, dtCommand);
                OCR = new EncoderOCR(Supplier, dtCommand);
                LoadPort = new EncoderLoadPort(Supplier, dtCommand, EncoderLoadPort.CommandMode.TDK_A);
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
