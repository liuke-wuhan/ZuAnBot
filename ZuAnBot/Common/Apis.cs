using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuAnBot.Common
{
    public class Apis
    {
        public static string GetZAWord()
        {
            var client = new RestClient("https://api.shadiao.app/");
            var request = new RestRequest("nmsl", Method.GET);
            request.AddParameter("level", "min");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var res = JObject.Parse(response.Content);

                return res["data"]["text"].ToString();
            }
            else
                return "连接服务器失败";
        }
    }
}
