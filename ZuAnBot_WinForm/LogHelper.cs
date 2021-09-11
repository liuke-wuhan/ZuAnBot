using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace ZuAnBot_WinForm
{
    public class Logger
    {
        private ILog _logger = null;
        private int _indentation = 0;

        public Logger(string logDirName = null, string loggerName = "liuke")
        {
            if (logDirName == null)
            {
                InitLogger("ZuanBot", loggerName);
            }
            else
            {
                InitLogger(logDirName, loggerName);
            }
        }

        /// <summary>
        /// 若没初始化，日志将放入%temp%/CECommonLog文件夹
        /// </summary>
        public ILog Logger4
        {
            get { return _logger; }
        }

        /// <summary>
        /// 初始化日志存放位置，若不初始化则存放在Temp/CECommonLog
        /// </summary>
        /// <param name="logDirName"></param>
        private void InitLogger(string logDirName, string loggerName)
        {
            string addinPath = typeof(Logger).Assembly.Location;
            string folder = Path.GetDirectoryName(addinPath);
            Uri uri = new Uri(Path.Combine(folder, "log4net_lk.config"), UriKind.Absolute);//日志配置文件位置

            string logFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TEMP", logDirName);
            if (!Directory.Exists(logFolder)) Directory.CreateDirectory(logFolder);
            log4net.GlobalContext.Properties["fname"] = Path.Combine(logFolder, "log");//日志存放位置

            log4net.Config.XmlConfigurator.Configure(uri);
            //创建一个实例，名字可随便取
            _logger = LogManager.GetLogger(loggerName);
            _logger.Info("==================初始化Logger====================");
        }

    }
}


