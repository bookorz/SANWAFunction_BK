using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANWA.Utility.Config
{
    public class SystemConfig
    {
        private static SystemConfig Content;
        public string DBConnectionString { get; set; }
        public string OCR1ImgSourcePath { get; set; }
        public string OCR1ImgToJpgPath { get; set; }
        public string OCR2ImgSourcePath { get; set; }
        public string OCR2ImgToJpgPath { get; set; }
        public string SystemMode { get; set; }
        public string OCR1ExePath { get; set; }
        public string OCR2ExePath { get; set; }
        public string MappingData { get; set; }
        public string EFEMInterfaceConn { get; set; }
        public bool SaftyCheckByPass { get; set; }

        public static SystemConfig Get()
        {
            if(Content == null)
            {
                ConfigTool<SystemConfig> SysCfg = new ConfigTool<SystemConfig>();
                Content = SysCfg.ReadFile("config/SystemConfig.json");
            }
            return Content;
        }

    }
}
