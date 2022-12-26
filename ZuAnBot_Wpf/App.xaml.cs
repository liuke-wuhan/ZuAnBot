using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using ImTools;
using Prism.DryIoc;
using Prism.Ioc;
using ZuAnBot_Wpf.Api;
using ZuAnBot_Wpf.Constants;
using ZuAnBot_Wpf.Helper;
using ZuAnBot_Wpf.ViewModels;
using ZuAnBot_Wpf.Views;

namespace ZuAnBot_Wpf
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            try
            {
                this.Startup += App_Startup;

                //wpf 程序异常捕获，而不崩溃退出
                DispatcherUnhandledException += App_DispatcherUnhandledException;
                TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            }
            catch (Exception e)
            {
                e.Show();
            }
        }

        #region 只运行一个程序
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:删除未读的私有成员", Justification = "<挂起>")]
        private System.Threading.Mutex mutex;
        private async void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                mutex = new System.Threading.Mutex(true, "ZuAnBot_Wpf", out bool ret);

                if (!ret)
                {
                    var handle = Win32Helper.FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, Titles.Main);
                    IPCHelper.SendMessage("显示", handle);
                    Environment.Exit(0);
                    return;
                }

                await Apis.GetInstance().Use();
            }
            catch (ApiException ex)
            {
#if DEBUG
                ex.Show();
#endif
            }
            catch (Exception ex)
            {
                ex.Show();
            }
        } 
        #endregion

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<WordsLibrarySet, WordsLibrarySetViewModel>();
            containerRegistry.RegisterDialog<WordEdit, WordEditViewModel>();
        }

        #region 异常处理
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            ExceptionHandler(e.Exception.InnerException);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            ExceptionHandler(e.Exception);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionHandler((Exception)e.ExceptionObject);
        }

        private void ExceptionHandler(Exception ex)
        {
            Dispatcher.Invoke(() =>
            {
                ex.Show();
            });
        }
        #endregion
    }
}
