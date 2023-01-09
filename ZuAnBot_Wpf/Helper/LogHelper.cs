using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace ZuAnBot_Wpf.Helper
{
    public static class LogHelper
    {
        private static ILog _logger = null;
        public static string logPath;

        static LogHelper()
        {
            InitLogger("ZuanBot", "ZuanBot");
        }

        /// <summary>
        /// 若没初始化，日志将放入%temp%\ZuanBot文件夹
        /// </summary>
        public static ILog Log
        {
            get { return _logger; }
        }

        /// <summary>
        /// 初始化日志存放位置，若不初始化则存放在%temp%\ZuanBot
        /// </summary>
        /// <param name="logDirName"></param>
        private static void InitLogger(string logDirName, string loggerName)
        {
            logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TEMP", logDirName);
            if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
            log4net.GlobalContext.Properties["fname"] = Path.Combine(logPath, "log");//日志存放位置

            var stream = ManifestHelper.GetManifestStream("log4net.config");
            log4net.Config.XmlConfigurator.Configure(stream);
            stream.Close();
            //创建一个实例，名字可随便取
            _logger = LogManager.GetLogger(loggerName);
            _logger.Info("==================初始化Logger====================");
        }

    }
}


