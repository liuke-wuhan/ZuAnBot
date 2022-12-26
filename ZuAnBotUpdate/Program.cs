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
            Console.WriteLine("args.Length：" + args.Length);
            if (args.Length != 2) return;

            var path1 = args[0];
            var path2 = args[1];

            Console.WriteLine("path1：" + path1);
            Console.WriteLine("path2：" + path2);
            if (string.IsNullOrWhiteSpace(path1) || string.IsNullOrWhiteSpace(path2)) return;

            var process = Process.GetProcessesByName("祖安助手").FirstOrDefault();

            if (process == null || process.MainModule?.FileName != path2) return;

            process.Kill();
            process.WaitForExit(1000);
            File.Copy(path1, path2, true);

            Process.Start(path2);

            Console.WriteLine("成功");
        }
    }
}