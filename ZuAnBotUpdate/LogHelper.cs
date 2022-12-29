using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace ZuAnBotUpdate
{
    public class LogHelper
    {
        private ILog _logger = null;
        public static string logPath;

        public LogHelper(string logDirName = null, string loggerName = "ZuanBot")
        {
            if (logDirName == null)
                InitLogger("ZuanBot", loggerName);
            else
            {
                InitLogger(logDirName, loggerName);
            }
        }

        public ILog Logger4
        {
            get { return _logger; }
        }

        /// <summary>
        /// 日志将放入%temp%/CECommonLog文件夹
        /// </summary>
        /// <param name="logDirName"></param>
        private void InitLogger(string logDirName, string loggerName)
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


