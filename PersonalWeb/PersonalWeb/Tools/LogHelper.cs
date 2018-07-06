using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PersonalWeb
{
    public class LogHelper
    {
        public static string filePath = "";
        public static string NewPath = "";
        /// <summary>
        /// 设置文件的路径
        /// </summary>
        private static void WriteLog(string log)
        {
            log4net.ILog myLogger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            myLogger.Info(log);
        }
    }
}