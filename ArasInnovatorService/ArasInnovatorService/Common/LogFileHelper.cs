using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ArasInnovatorService.Common
{
    public class LogFileHelper
    {
        private static readonly Object _thisLock = new Object();
        public static void ExcuteEventLog(string logFilePath, string strMSG)
        {
            lock (_thisLock)
            {
                using (FileStream fs = new FileStream(logFilePath, FileMode.Append, FileAccess.Write))
                {
                    StreamWriter wf = new StreamWriter(fs);
                    string strMessage = DateTime.Now.ToString("yyyy/MM/dd tt hh:mm:ss") + "\t" + strMSG;
                    wf.WriteLine(strMessage);
                    wf.Flush();
                    wf.Close();
                }
            }
        }
    }

    public static class LogFilePath
    {
        private static string _path;
        public static string path
        {
            get
            {
                if (string.IsNullOrEmpty(_path))
                {
                    _path = AppDomain.CurrentDomain.BaseDirectory + "log\\" + DateTime.Today.ToString("yyyyMMdd") + ".log";
                }
                return _path;
            }
        }
    }
}