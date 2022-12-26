using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace ZuAnBot_Wpf.Helper
{
    public class IPCHelper
    {
        /// <summary>
        /// 通过IPC发送消息到其他线程
        /// </summary>
        /// <param name="s">字符串消息</param>
        /// <param name="hWnd">目标窗口句柄</param>
        public static void SendMessage(string s, IntPtr hWnd)
        {
            if (hWnd == null) return;

            byte[] sarr = Encoding.Default.GetBytes(s);
            int len = sarr.Length;
            CopyData cds2;
            cds2.dwData = (IntPtr)0;
            cds2.cbData = len + 1;
            cds2.lpData = s;

            // 发送消息
            SendMessage(hWnd, WM_COPYDATA, IntPtr.Zero, ref cds2);
        }

        private static Action<string> _handler;
        public static void ObserveMessage(Window window, Action<string> handler)
        {
            _handler = handler;

            HwndSource hwndSource = PresentationSource.FromVisual(window) as HwndSource;
            if (hwndSource != null)
            {
                IntPtr handle = hwndSource.Handle;
                hwndSource.AddHook(new HwndSourceHook(WndProc));
            }
        }
        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_COPYDATA)
            {
                //int value = wParam.ToInt32();
                //var enumValue = (string)(object)value;

                CopyData cds = (CopyData)Marshal.PtrToStructure(lParam, typeof(CopyData));

                // 自定义行为
                var message = cds.lpData;
                _handler.Invoke(message);
            }
            return hwnd;
        }

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref CopyData lParam);
        public const int WM_COPYDATA = 0x004A; // 固定数值，不可更改

    }

    struct CopyData
    {
        public IntPtr dwData; // 任意值
        public int cbData;    // 指定lpData内存区域的字节数
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData; // 发送给目标窗口所在进程的数据
    }
}
