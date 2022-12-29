using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ZuAnBotUpdate
{
    public class Program
    {
        static void Main(string[] args)
        {
            var logger = new LogHelper().Logger4;
            try
            {
                logger.Info("进入更新程序args：" + args.Aggregate((a, b) => a + " " + b));
                if (args.Length != 2) return;

                var path1 = args[0];
                var path2 = args[1];

                if (string.IsNullOrWhiteSpace(path1) || string.IsNullOrWhiteSpace(path2)) return;

                var process = Process.GetProcessesByName("祖安助手").FirstOrDefault();

                if (process == null || process.MainModule?.FileName != path2) return;

                process.Kill();
                process.WaitForExit(1000);
                File.Copy(path1, path2, true);

                Process.Start(path2);

                logger.Info("更新程序执行成功");
            }
            catch (Exception e)
            {
                logger.Error("更新程序执行出错", e);
            }
        }
    }
}