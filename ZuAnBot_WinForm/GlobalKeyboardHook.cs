using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ZuAnBot_WinForm
{
    public class GlobalKeyboardHook
    {
        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 256;

        private const int WM_KEYUP = 257;

        private const int WM_SYSKEYDOWN = 260;

        private const int WM_SYSKEYUP = 261;

        private GlobalKeyboardHook.LLKeyboardHook llkh;

        public List<Keys> HookedKeys = new List<Keys>();

        private IntPtr Hook = IntPtr.Zero;

        public GlobalKeyboardHook()
        {
            this.llkh = new GlobalKeyboardHook.LLKeyboardHook(this.HookProc);
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern int CallNextHookEx(IntPtr hhk, int code, int wParam, ref GlobalKeyboardHook.keyBoardHookStruct lParam);

        ~GlobalKeyboardHook()
        {
            this.unhook();
        }

        public void hook()
        {
            IntPtr hInstance = GlobalKeyboardHook.LoadLibrary("User32");
            this.Hook = GlobalKeyboardHook.SetWindowsHookEx(13, this.llkh, hInstance, 0);
        }

        public int HookProc(int Code, int wParam, ref GlobalKeyboardHook.keyBoardHookStruct lParam)
        {
            if (Code >= 0)
            {
                Keys key = (Keys)lParam.vkCode;
                if (this.HookedKeys.Contains(key))
                {
                    KeyEventArgs kArg = new KeyEventArgs(key);
                    if ((wParam == 256 || wParam == 260) && this.KeyDown != null)
                    {
                        this.KeyDown(this, kArg);
                    }
                    else if ((wParam == 257 || wParam == 261) && this.KeyUp != null)
                    {
                        this.KeyUp(this, kArg);
                    }
                    if (kArg.Handled)
                    {
                        return 1;
                    }
                }
            }
            return GlobalKeyboardHook.CallNextHookEx(this.Hook, Code, wParam, ref lParam);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern IntPtr SetWindowsHookEx(int idHook, GlobalKeyboardHook.LLKeyboardHook callback, IntPtr hInstance, uint theardID);

        public void unhook()
        {
            GlobalKeyboardHook.UnhookWindowsHookEx(this.Hook);
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        public event KeyEventHandler KeyDown;

        public event KeyEventHandler KeyUp;

        public struct keyBoardHookStruct
        {
            public int vkCode;

            public int scanCode;

            public int flags;

            public int time;

            public int dwExtraInfo;
        }

        public delegate int LLKeyboardHook(int Code, int wParam, ref GlobalKeyboardHook.keyBoardHookStruct lParam);
    }
}
