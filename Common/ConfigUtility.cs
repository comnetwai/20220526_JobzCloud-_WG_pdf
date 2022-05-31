using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;


namespace Common
{
    public class ConfigUtility
    {
        private static string strConfigDir;
        private static Hashtable confDBSetting;

        public static string GetDBSettingConfig(string Key)
        {
            string strPath = Path.Combine(GetConfigDirectory(), GetWebConfig("DB_SETTING_FILE"));

            if (confDBSetting == null)
                confDBSetting = GetBaseConfig(strPath, null);
            return confDBSetting[Key].ToString();
        }

        public static string GetConfigDirectory()
        {
            if (strConfigDir == null)
            {
                strConfigDir = (System.Web.HttpContext.Current.Server.MapPath("~/") + GetWebConfig("CONFIG_DIRECTORY"));
            }
            return strConfigDir;
        }
        public static string GetWebConfig(string Key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[Key];
        }

        public static Hashtable GetBaseConfig(string strPath, Type typParam)
        {
            Hashtable htXml = null;
            if (System.IO.File.Exists(strPath))
            {
                htXml = XmlUtility.LoadXml(strPath, typParam);
            }

            return htXml;
        }
    }
}
