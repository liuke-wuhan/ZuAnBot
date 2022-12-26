using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using ZuAnBot_Wpf.Constants;
using ZuAnBot_Wpf.Helper;

namespace ZuAnBot_Wpf.ViewModels
{
    /// <summary>
    /// 词库
    /// </summary>
    /// <remarks>后期可考虑分为视图模型和模型</remarks>
    public class WordsLibrarySetViewModel : BindableBase, IDialogAware
    {
        private WordsLibrary _Library;
        /// <summary>
        /// 词库
        /// </summary>
        public WordsLibrary Library
        {
            get { return _Library; }
            set { SetProperty(ref _Library, value); }
        }

        #region IDialogAware
        public string Title => "词库定义";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() { return true; }

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            try
            {
                Library = parameters.GetValue<WordsLibrary>(Params.Library);
            }
            catch (Exception e)
            {
                e.Show();
            }
        }
        #endregion IDialogAware

        #region SaveCommand
        private DelegateCommand _SaveCommand;
        public DelegateCommand SaveCommand => _SaveCommand ?? (_SaveCommand = new DelegateCommand(ExecuteSaveCommand));
        void ExecuteSaveCommand()
        {
            try
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
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
