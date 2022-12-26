using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using ZuAnBot_Wpf.Constants;
using ZuAnBot_Wpf.Helper;

namespace ZuAnBot_Wpf.ViewModels
{
    public class WordEditViewModel : BindableBase, IDialogAware
    {
        private string _Content;
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set
            {
                SetProperty(ref _Content, value);
            }
        }

        #region Dialog
        public string Title => "词条";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() { return true; }

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Content = parameters.GetValue<string>(Params.WordContent);
        }
        #endregion Dialog

        #region OkCommand
        private DelegateCommand _OkCommand;
        public DelegateCommand OkCommand => _OkCommand ?? (_OkCommand = new DelegateCommand(ExecuteOkCommand));
        void ExecuteOkCommand()
        {
            try
            {
                WordsHelper.CheckContent(Content);

                var parameters = new DialogParameters();
                parameters.Add(Params.WordContent, Content);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameters));
            }
            catch (ArgumentOutOfRangeException e)
            {
                e.Show(showDetail: false);
            }
            catch (Exception e)
            {
                e.Show();
            }
        }
        #endregion

        #region CancelCommand
        private DelegateCommand _CancelCommand;
        public DelegateCommand CancelCommand => _CancelCommand ?? (_CancelCommand = new DelegateCommand(ExecuteCancelCommand));
        void ExecuteCancelCommand()
        {
            try
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
            }
            catch (Exception e)
            {
                e.Show();
            }
        }
        #endregion


    }
}
