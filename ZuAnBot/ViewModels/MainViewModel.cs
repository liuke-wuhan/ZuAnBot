using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ZuAnBot.Common;

namespace ZuAnBot.ViewModels
{
    public class MainViewModel : BindableBase
    {
        ZuAnHelper zuAnHelper;

        public MainViewModel()
        {
            zuAnHelper = new ZuAnHelper();
        }

        private bool state;
        public bool State
        {
            get { return state; }
            set
            {
                //MessageBox.Show(Apis.GetZAWord());
                zuAnHelper.SetStatus(value);
                SetProperty(ref state, value);
            }
        }
    }
}
