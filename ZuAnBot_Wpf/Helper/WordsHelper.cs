using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using ZuAnBot_Wpf.ViewModels;

namespace ZuAnBot_Wpf.Helper
{
    public class WordsHelper
    {

        public static string GetZAWord()
        {
            var client = new RestClient("https://api.shadiao.app/");
            var request = new RestRequest("nmsl", Method.Get);
            request.AddParameter("level", "min");
            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var res = JObject.Parse(response.Content);

                return res["data"]["text"].ToString();
            }
            else
                return "连接服务器失败";
        }


        public static void CheckContent(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentOutOfRangeException("词条内容不为空", innerException: null);
            }
            if (value.Length > 200)
            {
                throw new ArgumentOutOfRangeException("词条内容不能超过200", innerException: null);
            }
        }
    }
}
