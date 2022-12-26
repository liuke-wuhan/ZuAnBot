using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            hook.HookedKeys.Add(Keys.F10);
            hook.HookedKeys.Add(Keys.F11);
            hook.HookedKeys.Add(Keys.F12);
#if DEBUG
            hook.HookedKeys.Add(Keys.Delete);
#endif
            hook.hook();
        }

        private void Hook_KeyUp(object sender, KeyEventArgs e)
        {
            string word = checkBox_all.Checked ? "all/ " : "";
            if (e.KeyCode == Keys.F2)
                word += Apis.GetLoacalWord(Apis.WordType.zuanMin);
            else if (e.KeyCode == Keys.F3)
                word += Apis.GetLoacalWord(Apis.WordType.zuanMax);
            else if (e.KeyCode == Keys.F10)
            {
                this.checkBox_all.Checked = !this.checkBox_all.Checked;
                return;
            }
            else if (e.KeyCode == Keys.F11)
            {
                this.checkBox_perWord.Checked = !this.checkBox_perWord.Checked;
                return;
            }
            else if (e.KeyCode == Keys.F12)
            {
                Program.logger.Error($"上个语录被屏蔽");
                return;
            }
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

            var builder = Simulate.Events();
            if (checkBox_perWord.Checked)
            {
                foreach (var item in word)
                {
                    builder = builder.
                        Click(WindowsInput.Events.KeyCode.Enter).Wait(75).
                        Click(item).Wait(75).
                        Click(WindowsInput.Events.KeyCode.Enter).Wait(75);

                }
            }
            else
            {
                builder = builder.
                    Click(WindowsInput.Events.KeyCode.Enter).Wait(75).
                    Click(word).Wait(75).
                    Click(WindowsInput.Events.KeyCode.Enter).Wait(75);
            }
            builder.Invoke();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            hook.unhook();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //notifyIcon1.Visible = true;
                Hide();

                using (var writter = File.CreateText(Path.Combine(Logger.logPath, "句柄.txt")))
                {
                    writter.Write(this.Handle.ToInt32().ToString());
                }
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            //notifyIcon1.Visible = false;
            Show();
            WindowState = FormWindowState.Normal;
            Focus();
        }

        /// <summary>
        /// 接收IPC消息，用于确保只打开一个程序
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == IPCHelper.WM_COPYDATA)
            {
                CopyData cds = (CopyData)Marshal.PtrToStructure(m.LParam, typeof(CopyData)); // 接收封装的消息
                string message = cds.lpData; // 获取消息内容
                // 自定义行为
                if (message == "显示")
                {
                    notifyIcon1_DoubleClick(this, null);
                }
                else if (message == "隐藏")
                {
                    Form1_SizeChanged(this, null);
                }
            }

            base.WndProc(ref m);
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1_DoubleClick(this, null);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
