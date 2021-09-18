using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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

            //1.这里判定是否已经有实例在运行
            Process instance = RunningInstance();
            logger = new Logger().Logger4;
            if (instance == null)
            {
                //1.1 没有实例在运行
                Application.Run(new Form1());
            }
            else
            {
                //1.2 已经有一个实例在运行
                HandleRunningInstance(instance);
            }
        }

        //2.在进程中查找是否已经有实例在运行
        #region 确保程序只运行一个实例
        private static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //遍历与当前进程名称相同的进程列表
            foreach (Process process in processes)
            {
                //如果实例已经存在则忽略当前进程
                if (process.Id != current.Id)
                {
                    //保证要打开的进程同已经存在的进程来自同一文件路径
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        //返回已经存在的进程
                        return process;
                    }
                }
            }
            return null;
        }

        //3.已经有了就把它激活，并将其窗口放置最前端
        private static void HandleRunningInstance(Process instance)
        {
            var handle = Int32.Parse(File.ReadAllText(Path.Combine(Logger.logPath, "句柄.txt")));
            IPCHelper.SendMessage("显示", new IntPtr(handle));
        }


        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(System.IntPtr hWnd);

        #endregion
    }
}
