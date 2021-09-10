using Prism.Mvvm;

namespace ZuAnBot.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "欢迎来到祖安！";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
