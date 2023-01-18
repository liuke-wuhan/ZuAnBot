using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using WindowsInput;
using ZuAnBot_Wpf.Api;
using ZuAnBot_Wpf.Constants;
using ZuAnBot_Wpf.Helper;
using ZuAnBot_Wpf.Views;

namespace ZuAnBot_Wpf.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        private readonly Apis _apis = Apis.GetInstance();
        GlobalKeyboardHook hook;

        public WordsLibrary Library { get; set; }

        #region 绑定属性
        private bool _IsPerWord = false;
        /// <summary>
        /// 是否逐字发送
        /// </summary>
        public bool IsPerWord
        {
            get { return _IsPerWord; }
            set { SetProperty(ref _IsPerWord, value); }
        }

        private bool _IsAll = false;
        /// <summary>
        /// 是否发送所有人消息
        /// </summary>
        public bool IsAll
        {
            get { return _IsAll; }
            set { SetProperty(ref _IsAll, value); }
        }


        private bool _IsNotifyIconBlink;
        /// <summary>
        /// 托盘图标是否闪烁
        /// </summary>
        public bool IsNotifyIconBlink
        {
            get { return _IsNotifyIconBlink; }
            set { SetProperty(ref _IsNotifyIconBlink, value); }
        }

        private bool _IsNotifyIconShow = true;
        /// <summary>
        /// 托盘图标是否显示
        /// </summary>
        public bool IsNotifyIconShow
        {
            get { return _IsNotifyIconShow; }
            set { SetProperty(ref _IsNotifyIconShow, value); }
        }

        private string _Version;
        /// <summary>
        /// 程序版本号
        /// </summary>
        public string Version
        {
            get { return _Version; }
            set { SetProperty(ref _Version, value); }
        }

        private bool _NeedUpdate;
        /// <summary>
        /// 是否需要更新
        /// </summary>
        public bool NeedUpdate
        {
            get { return _NeedUpdate; }
            set { SetProperty(ref _NeedUpdate, value); }
        }

        #endregion 绑定属性

        public MainWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        #region 命令
        #region LoadedCommand
        private DelegateCommand _LoadedCommand;
        public DelegateCommand LoadedCommand => _LoadedCommand ?? (_LoadedCommand = new DelegateCommand(ExecuteLoadedCommand));
        async void ExecuteLoadedCommand()
        {
            try
            {
                LoadWordsLibrary();

                HookKeys();

                Version = "v" + System.Windows.Application.ResourceAssembly.GetName().Version.ToString(3);

                await InitNeedUpdate();
            }
            catch (Exception e)
            {
                e.Show();
            }
        }

        private async Task InitNeedUpdate()
        {
            var latestVersion = await VersionHelper.GetLatestVersion();
            NeedUpdate = !VersionHelper.IsNewestVersion(latestVersion);
        }

        private void LoadWordsLibrary()
        {
            try
            {
                //第一试用本软件没有本地词库，用资源清单的词库
                if (!File.Exists(LocalConfigHelper.WordsLibraryPath))
                {
                    var dir = Path.GetDirectoryName(LocalConfigHelper.WordsLibraryPath);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    var manifestStream = ManifestHelper.GetManifestStream("wordsLibrary.json");
                    using (var stream = File.Create(LocalConfigHelper.WordsLibraryPath))
                    {
                        manifestStream.CopyTo(stream);
                    }
                }

                Library = JsonHelper.DeserializeWordsLibrary();//反序列化本地词库
            }
            catch (Exception e)
            {
                e.Show("读取词库失败！");
                File.Delete(LocalConfigHelper.WordsLibraryPath);
                App.Current.Shutdown();
            }
        }

        #endregion

        #region 访问GitHub
        private DelegateCommand _VisitGitHubCommand;
        public DelegateCommand VisitGitHubCommand => _VisitGitHubCommand ?? (_VisitGitHubCommand = new DelegateCommand(ExecuteVisitGitHubCommand));
        void ExecuteVisitGitHubCommand()
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/liuke-wuhan/ZuAnBot");
            }
            catch (Exception e)
            {
                e.Show();
            }
        }
        #endregion

        #region SetCommand
        private DelegateCommand _SetCommand;

        public DelegateCommand SetCommand => _SetCommand ?? (_SetCommand = new DelegateCommand(ExecuteSetCommand));
        void ExecuteSetCommand()
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add(Params.Library, Library);
                IDialogResult r = null;
                _dialogService.ShowDialog(nameof(WordsLibrarySet), parameters, result => r = result);

                if (r.Result == ButtonResult.OK)
                {
                    JsonHelper.SerializeWordsLibrary(Library);
                }
                else
                {
                    LoadWordsLibrary();
                }
            }
            catch (Exception e)
            {
                e.Show();
            }
        }
        #endregion

        #region UpdateCommand
        private bool _UpdateEnabled = true;
        public bool UpdateEnabled
        {
            get { return _UpdateEnabled; }
            set { SetProperty(ref _UpdateEnabled, value); }
        }
        private DelegateCommand _UpdateCommand;
        public DelegateCommand UpdateCommand => _UpdateCommand ?? (_UpdateCommand = new DelegateCommand(ExecuteUpdateCommand).ObservesCanExecute(() => UpdateEnabled));
        async void ExecuteUpdateCommand()
        {
            try
            {
                UpdateEnabled = false;

                var latestVersion = await VersionHelper.GetLatestVersion();
                if (VersionHelper.IsNewestVersion(latestVersion))
                {
                    MessageHelper.Info($"当前版本已经是最新版本");
                }
                else
                {
                    var result = MessageHelper.Question($"当前版本为{VersionHelper.GetCurrentVersionName()}，最新版本为{latestVersion.VersionName}，是否更新？");
                    if (result)
                    {
                        var tempDir = Path.GetTempPath();//临时目录
                        var file = _apis.DownloadFile(tempDir, latestVersion.FileName, latestVersion.Url);//下载到临时目录

                        //使用更新
                        var assembly = Assembly.GetExecutingAssembly();
                        var stream = assembly.GetManifestResourceStream("costura.zuanbotupdate.exe");
                        var tempPath = Path.Combine(Path.GetTempPath(), "zuanbotupdate.exe");
                        using (var tempStream = File.Create(tempPath))
                        {
                            stream.CopyTo(tempStream);
                        }

                        var startInfo = new ProcessStartInfo(tempPath, $"{file} {assembly.Location}");
                        //设置不在新窗口中启动新的进程
                        startInfo.CreateNoWindow = true;
                        //不使用操作系统使用的shell启动进程
                        startInfo.UseShellExecute = false;
                        //将输出信息重定向
                        startInfo.RedirectStandardOutput = true;
                        Process.Start(startInfo);
                    }
                }
            }
            catch (Exception e)
            {
                e.Show();
            }
            finally
            {
                UpdateEnabled = true;
            }
        }
        #endregion


        #endregion

        /// <summary>
        /// 按键勾子
        /// </summary>
        private void HookKeys()
        {
            hook = new GlobalKeyboardHook();
            hook.KeyUp += Hook_KeyUp;
            hook.HookedKeys.Add(Keys.F2);
            hook.HookedKeys.Add(Keys.F3);
            hook.HookedKeys.Add(Keys.F11);
            hook.HookedKeys.Add(Keys.F12);
            hook.hook();
        }

        /// <summary>
        /// 勾子事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hook_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                string word = "";
                if (e.KeyCode == Keys.F2)
                    word += Library.GetLoacalWord("默认词库");
                else if (e.KeyCode == Keys.F3)
                    word += Library.GetLoacalWord("自定义词库");
                else if (e.KeyCode == Keys.F11)
                {
                    IsAll = !IsAll;
                    return;
                }
                else if (e.KeyCode == Keys.F12)
                {
                    IsPerWord = !IsPerWord;
                    return;
                }
                else
                {
                    return;
                }

                string allPre = IsAll ? "/all " : "";

                var builder = Simulate.Events();
                if (IsPerWord)
                {
                    foreach (var item in word)
                    {
                        builder = builder.
                            Click(WindowsInput.Events.KeyCode.Enter).Wait(100).
                            Click(allPre + item).Wait(100).
                            Click(WindowsInput.Events.KeyCode.Enter).Wait(100);

                    }
                }
                else
                {
                    builder = builder.
                        Click(WindowsInput.Events.KeyCode.Enter).Wait(100).
                        Click(allPre + word).Wait(100).
                        Click(WindowsInput.Events.KeyCode.Enter).Wait(100);
                }
                builder.Invoke();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageHelper.Error($"词库为空");
            }
            catch (Exception ex)
            {
                ex.Show();
            }
        }

    }
}
