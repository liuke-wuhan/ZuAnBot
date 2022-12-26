using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZuAnBot_Wpf.Constants;

namespace ZuAnBot_Wpf.Helper
{
    /// <summary>
    /// win32帮助类
    /// </summary>
    public static class Win32Helper
    {
        public static bool SetWindowForeground(string winTitle)
        {
            var hWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, winTitle);

            if (hWnd != IntPtr.Zero)
            {
                SetForegroundWindow(hWnd); //Activate it
                return true;
            }

            return false;
        }

        public static bool SetMainWindowForeground()
        {
            return SetWindowForeground(Titles.Main);
        }

        public static bool CloseWindow(string winTitle)
        {
            var hWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, winTitle);

            if (hWnd != IntPtr.Zero)
            {
                SendMessage(hWnd, WM_SYSCOMMAND, SC_CLOSE, 0);
                return true;
            }

            return false;
        }


        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr handleParent, IntPtr handleChild, string className, string WindowName);

        /// <summary>
        /// 获取窗体的句柄函数
        /// </summary>
        /// <param name="lpClassName">窗口类名</param>
        /// <param name="lpWindowName">窗口标题名</param>
        /// <returns>返回句柄</returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll")]
        private static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        const int WM_SYSCOMMAND = 0x0112;
        const int SC_CLOSE = 0xF060;
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
    }
}
