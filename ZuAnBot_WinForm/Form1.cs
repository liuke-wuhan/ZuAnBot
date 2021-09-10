using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;

namespace ZuAnBot_WinForm
{
    public partial class Form1 : Form
    {
        GlobalKeyboardHook hook;
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Alt, Keys.Tab);

            hook = new GlobalKeyboardHook();
            hook.KeyDown += Hook_KeyDown;
            hook.HookedKeys.Add(Keys.Delete);            
            hook.hook();
        }

        private void Hook_KeyDown(object sender, KeyEventArgs e)
        {
            var word = Apis.GetZAWord();

            #region 打字
            Simulate.Events().Click(WindowsInput.Events.KeyCode.Enter).Wait(75).Click(word).Wait(75).Click(WindowsInput.Events.KeyCode.Enter).Invoke();
            #endregion

            //MessageBox.Show("勾子");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //HotKey.UnregisterHotKey(Handle, 101);
            hook.unhook();
        }

        protected override void WndProc(ref Message m)
        {

            const int WM_HOTKEY = 0x0312;
            //按快捷键 
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:
                            //MessageBox.Show("按下的是Shift+S");    
                            break;
                        case 101:
                            //var word = Apis.GetZAWord().Result;

                            //Clipboard.SetDataObject(word);
                            //var key = new KeyboardKey(Keys.V);
                            //key.PressAndRelease(true, false);
                            //new KeyboardKey(Keys.Enter).PressAndRelease(false, false);

                            MessageBox.Show("按下的是Shift+S");
                            break;
                        case 102:
                            //MessageBox.Show("按下的是Alt+D");
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }


    }
}
