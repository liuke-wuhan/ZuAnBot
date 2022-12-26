using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HandyControl.Controls;
using ZuAnBot_Wpf.Api;
using MessageBox = HandyControl.Controls.MessageBox;

namespace ZuAnBot_Wpf.Helper
{
    public static class ExceptionExtension
    {
        /// <summary>
        /// 获取错误描述
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetDescription(this Exception ex)
        {
            return GetDescriptionInternal(ex);
        }

        private static string GetDescriptionInternal(Exception ex, int n = 1)
        {
            string innerError = "";
            if (ex.InnerException != null && n < 5)
            {
                var innerDesc = GetDescriptionInternal(ex.InnerException, ++n);
                innerError = $"\n--- 内部错误 ---\n：{innerDesc}";
            }

            return $"{ex.GetSourceDesc()}{ex.Message}\n[{ex.GetType().FullName}]调用堆栈：\n{ex.StackTrace}{innerError}";
        }

        /// <summary>
        /// 描述异常来源，是后端还是客户端
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetSourceDesc(this Exception ex)
        {
            if (ex is ApiException)
            {
                return "[服务器异常]";
            }
            else if(ex is HttpRequestException)
            {
                return "[服务器异常]";
            }
            else
            {
                return "[客户端异常]";
            }
        }

        /// <summary>
        /// 通过messagebox弹出错误
        /// </summary>
        /// <param name="e"></param>
        public static void Show(this Exception e, string msg = "", bool showDetail = true)
        {
            if (!string.IsNullOrWhiteSpace(msg))
                msg += "\n";

            if (showDetail)
                msg += e.GetDescription();
            else
                msg += e.Message;

            MessageHelper.Error(msg);
        }
    }
}
