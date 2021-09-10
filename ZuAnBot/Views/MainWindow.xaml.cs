using MahApps.Metro.Controls;
using Prism.Regions;
using System.Windows;
using System.Windows.Interop;
using System;
using ZuAnBot.Common;
using HotKey = ZuAnBot.Common.HotKey;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace ZuAnBot.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        IntPtr handle;
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            regionManager.RegisterViewWithRegion("MainRegion", typeof(Main));
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (hwndSource != null)
            {
                handle = hwndSource.Handle;
                hwndSource.AddHook(new HwndSourceHook(WndProc));
            }
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 0x0312)
            {
                switch (wParam.ToInt32())
                {
                    case 100:    //按下的是 Ctrl+Z
                        MessageBox.Show("按下Ctrl+Z");
                        break;

                    default:
                        break;
                }
            }
            return hwnd;
        }

        public void Register()
        {
            //注册热键Shift+S，Id号为100。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。
           HotKey.RegisterHotKey(handle, 100, HotKey.KeyModifiers.Ctrl, Keys.Z);
        }

        public void UnRegister()
        {
            //注销Id号为100的热键设定
            HotKey.UnregisterHotKey(handle, 100);
        }

        private void MetroWindow_Activated(object sender, EventArgs e)
        {
            Register();
        }

        private void MetroWindow_Deactivated(object sender, EventArgs e)
        {
            UnRegister();
        }
    }
}
