using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZuAnBot_Wpf.Helper
{
    public static class MessageHelper
    {
        public static void Error(string msg)
        {
            MessageBox.Show(msg, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void Info(string msg)
        {
            MessageBox.Show(msg, "通知", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 警告并询问用户是否继续
        /// </summary>
        /// <param name="msg"></param>
        /// <returns>是否继续</returns>
        public static bool Warning(string msg)
        {
            return MessageBox.Show(msg, "警告", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK;
        }

        /// <summary>
        /// 询问用户是否继续
        /// </summary>
        /// <param name="msg"></param>
        /// <returns>是否继续</returns>
        public static bool Question(string msg)
        {
            return MessageBox.Show(msg, "", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK;
        }
    }
}
