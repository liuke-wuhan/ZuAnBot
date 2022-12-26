using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuAnBot_Wpf.Helper
{
    /// <summary>
    /// 本地配置文件
    /// </summary>
    public class LocalConfigHelper
    {
        private static readonly string wordsLibraryFile = "wordsLibrary.json";

        /// <summary>
        /// 配置文件目录
        /// </summary>
        public static string Dir => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ZuAnBot");

        /// <summary>
        /// 词库文件路径
        /// </summary>
        public static string WordsLibraryPath => GetPath(wordsLibraryFile);

        /// <summary>
        /// 获取配置文件路径
        /// </summary>
        /// <param name="configFileName"></param>
        /// <returns></returns>
        public static string GetPath(string configFileName)
        {
            return Path.Combine(Dir, configFileName);
        }


    }
}
