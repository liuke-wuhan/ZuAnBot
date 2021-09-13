using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuAnBot_WinForm
{
    public class Apis
    {
        static Words zuanWords_min;
        static Words zuanWords_max;
        static Words caiHongPi;

        static Apis()
        {
            zuanWords_min = ManifestResourceUtils.GetJsonObject<Words>("zuanWords_min.json");
            zuanWords_max = ManifestResourceUtils.GetJsonObject<Words>("zuanWords_max.json");
            caiHongPi = ManifestResourceUtils.GetJsonObject<Words>("caiHongPi.json");
        }

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

        public static string GetLoacalWord(WordType type)
        {
            Words words;
            switch (type)
            {
                case WordType.zuanMin:
                    words = zuanWords_min;
                    break;
                case WordType.zuanMax:
                    words = zuanWords_max;
                    break;
                case WordType.chp:
                    words = caiHongPi;
                    break;
                default:
                    words = caiHongPi;
                    break;
            }

            Random random = new Random((int)DateTime.Now.Ticks);
            var word = words.words[random.Next(0, words.words.Count)];

            return word;
        }

        static int i = 0;
        public static string GetTestWord(out int index)
        {
            index = i;
            if (i >= zuanWords_min.words.Count) return "测试完毕！";

            Words words = zuanWords_min;

            var word = words.words[i++];

            return word;
        }

        public enum WordType
        {
            zuanMin, zuanMax, chp
        }
    }
}
