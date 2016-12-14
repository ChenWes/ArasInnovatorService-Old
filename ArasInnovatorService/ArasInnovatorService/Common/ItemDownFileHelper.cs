using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ArasInnovatorService.Common
{
    public static class ItemDownFilePath
    {
        private static string _path;
        public static string path
        {
            get
            {
                if (string.IsNullOrEmpty(_path))
                {
                    _path = AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetAPPConfigValue("tempFileFolder") + "\\";
                }
                return _path;
            }
        }
    }
}