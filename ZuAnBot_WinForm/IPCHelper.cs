using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZuAnBot_WinForm
{
    public class IPCHelper
    {
        private static Action<string> _messageHandler;

        /// <summary>
        /// 通过IPC发送消息到其他线程
        /// </summary>
        /// <param name="s">字符串消息</param>
        /// <param name="hWnd">目标窗口句柄</param>
        public static void SendMessage(string s, IntPtr hWnd)
        {
            byte[] sarr = Encoding.Default.GetBytes(s);
            int len = sarr.Length;
            CopyData cds2;
            cds2.dwData = (IntPtr)0;
            cds2.cbData = len + 1;
            cds2.lpData = s;

            // 发送消息
            SendMessage(hWnd, WM_COPYDATA, IntPtr.Zero, ref cds2);
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
