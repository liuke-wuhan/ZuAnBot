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
using ZuAnBot_WinForm.Properties;

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
            var words = ManifestResourceUtils.GetJsonObject<Words>("zuanWords_min.json");

            hook = new GlobalKeyboardHook();
            hook.KeyUp += Hook_KeyUp;
            hook.HookedKeys.Add(Keys.F2);
            hook.HookedKeys.Add(Keys.F3);
            hook.HookedKeys.Add(Keys.F4);
#if DEBUG
            hook.HookedKeys.Add(Keys.F12);
#endif
            hook.hook();
        }

        private void Hook_KeyUp(object sender, KeyEventArgs e)
        {
            string word = "";
            if (e.KeyCode == Keys.F2)
                word = Apis.GetLoacalWord(Apis.WordType.zuanMin);
            else if (e.KeyCode == Keys.F3)
                word = Apis.GetLoacalWord(Apis.WordType.zuanMax);
            else if (e.KeyCode == Keys.F4)
                word = Apis.GetLoacalWord(Apis.WordType.chp);
#if DEBUG
            else
            {
                int index;
                word = Apis.GetTestWord(out index);
                Program.logger.Info($"index：{index}。消息：{word}");
            }
#endif

#if !DEBUG
            Program.logger.Info($"按键：{e.KeyCode}。消息：{word}");
#endif


            Simulate.Events().
                Click(WindowsInput.Events.KeyCode.Enter).Wait(75).
                Click(word).Wait(75).
                Click(WindowsInput.Events.KeyCode.Enter).
                Invoke();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            hook.unhook();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
