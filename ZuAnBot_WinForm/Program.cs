using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZuAnBot_WinForm
{
    static class Program
    {
        public static log4net.ILog logger;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            logger = new Logger().Logger4;

            Application.Run(new Form1());
        }
    }
}
