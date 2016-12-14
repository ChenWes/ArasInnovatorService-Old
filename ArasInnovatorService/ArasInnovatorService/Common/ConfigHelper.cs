
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.Common
{
    public class ConfigHelper
    {
        public static string GetCurrentBaseDirectory()
        {
            return GetCurrentBaseDirectory(true);
        }

        public static string GetCurrentBaseDirectory(bool withEndflag)
        {
            string s = AppDomain.CurrentDomain.BaseDirectory;
            if (withEndflag)
            {
                return s;
            }
            return s.Substring(0, s.Length - 1);
        }

        public static string GetAPPConfigValue(string configKey)
        {
            return System.Configuration.ConfigurationManager.AppSettings[configKey];
        }

        public static string GetConnectionString(string cKey)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[cKey].ToString();
        }

        public static string GetGlobalizationDateTimeShortFormat()
        {
            if (System.Globalization.DateTimeFormatInfo.CurrentInfo != null)
                return System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
            return null;
        }
    }
}