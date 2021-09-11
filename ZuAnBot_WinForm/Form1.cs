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
            var words = ManifestResourceUtils.GetJsonObject<Words>("zuanWords_min.json");

            hook = new GlobalKeyboardHook();
            hook.KeyDown += Hook_KeyDown;
            hook.HookedKeys.Add(Keys.Delete);            
            hook.hook();
        }

        private void Hook_KeyDown(object sender, KeyEventArgs e)
        {
            var word = Apis.GetLoacalWord(Apis.WordType.zuanMin);

            Simulate.Events().Click(WindowsInput.Events.KeyCode.Enter).Wait(75).Click(word).Wait(75).Click(WindowsInput.Events.KeyCode.Enter).Invoke();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            hook.unhook();
        }
    }
}
