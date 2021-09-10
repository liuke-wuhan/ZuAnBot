using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Gma.System.MouseKeyHook;

namespace ZuAnBot.Common
{
    public class ZuAnHelper
    {
        IKeyboardMouseEvents keyboardMouseEvents;
        public ZuAnHelper()
        {
            keyboardMouseEvents = Hook.GlobalEvents();
        }

        //static bool _status = false;

        public void SetStatus(bool status)
        {

            //设置快捷键
            if (status)
            {
                keyboardMouseEvents.KeyUp += ZuAnHelper_KeyDown;
            }
            else
            {
                keyboardMouseEvents.KeyUp -= ZuAnHelper_KeyDown;
            }
        }

        private void KeyboardMouseEvents_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void KeyboardMouseEvents_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                var word = Apis.GetZAWord();
                Clipboard.SetData(DataFormats.Text, word);
                //KeyboardKey key = new KeyboardKey(System.Windows.Forms.Keys.V);
                //key.PressAndRelease(false, false);
                MessageBox.Show(word);
            }
        }

        private void ZuAnHelper_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if ( e.KeyCode == System.Windows.Forms.Keys.O)
            {
                var word = Apis.GetZAWord();
                Clipboard.SetData(DataFormats.Text, word);
                //KeyboardKey key = new KeyboardKey(System.Windows.Forms.Keys.V);
                //key.PressAndRelease(false, false);
                MessageBox.Show(word);
            }
        }
    }
}
