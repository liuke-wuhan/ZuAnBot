using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using ZuAnBot_Wpf.Api.Dtos;
using ZuAnBot_Wpf.Helper;

namespace ZuAnBot_Wpf.Api
{
    public class Apis
    {
        readonly RestClient _client;

        #region Construct
        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static Apis GetInstance() => instance;
        private static readonly Apis instance = new Apis();
        private Apis()
        {
            try
            {
                _client = ConstructClient(EnvConfigHelper.Env.UseApi);
            }
            catch (Exception ex)
            {
                ex.Show();
            }
        }
        private RestClient ConstructClient(string baseUrl)
        {
            var client = new RestClient(baseUrl);
            client.Options.MaxTimeout = 120000;
            client.Options.ThrowOnAnyError = false;

            client.UseNewtonsoftJson(new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            });
            client.AddDefaultHeader("Accept", "application/json");
            client.AddDefaultHeader("Content-Type", "application/json");
            return client;
        }
        #endregion Construct

        #region private
        /// <summary>
        /// api响应包装
        /// code 编号 200 对应 返回 result  code 编号 0 对应 返回 data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        class ResultWrapper<T>
        {
            public int? Code { get; set; }
            public string Msg { get; set; }
            public T Data { get; set; }
        }

        private T HandleResponseOrError<T>(RestResponse<ResultWrapper<T>> response, RestClient client)
        {
            var url = (client.Options.BaseUrl + response.Request.Resource).Replace("//", "/");

            if (response.IsSuccessful)
            {
                if (response.Data.Code == 200)
                {
                    //LkLogHelper.Log.Info($"〔{url}〕请求成功，返回值：〔{JsonConvert.SerializeObject(response.Data.Result)}〕");
                    return response.Data.Data;
                }
                else
                {
                    throw new ApiException($"code：{response.Data.Code}\nmessage：{response.Data.Msg}", new Exception($"url：{url}"));
                }

            }
            else
            {
                if (response.ContentType == "text/plain")
                    throw new ApiException($"({(int)response.StatusCode} {response.StatusDescription}){response.Content}", new Exception($"url：{url}"));
                else
                {
                    if (response.StatusCode == 0 && response.ErrorMessage == "第 3 行，位置 6 上的开始标记“meta”与结束标记“head”不匹配。 第 107 行，位置 5。")
                    {
                        throw new ApiException($"后台服务不可用", new Exception($"url：{url}"));
                    }
                    else if (response.StatusCode == 0 && response.ErrorMessage == "The request timed-out.")
                    {
                        throw new ApiException($"请求超时", new Exception($"url：{url}"));
                    }
                    else
                    {
                        throw new ApiException($"({(int)response.StatusCode} {response.StatusDescription}){response.ErrorMessage}", new Exception($"url：{url}"));
                    }
                }

            }
        }
        #endregion

        /// <summary>
        /// 文件备份
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task Use()
        {
            var request = new RestRequest("/api/auth/use", Method.Post);

            var response = await _client.ExecuteAsync<ResultWrapper<string>>(request);

            HandleResponseOrError(response, _client);
        }

        /// <summary>
        /// 获取最新版本
        /// </summary>
        /// <returns></returns>
        public async Task<VersionDto> GetLatestVersion()
        {
            var request = new RestRequest("/api/versions/latest", Method.Get);

            var response = await _client.ExecuteAsync<ResultWrapper<VersionDto>>(request);

            return HandleResponseOrError(response, _client);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="dir">目录</param>
        /// <param name="fileName">文件名</param>
        /// <param name="fileUrl">url</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">输入字符串为空</exception>
        /// <exception cref="Exception">文件下载失败</exception>
        public string DownloadFile(string dir, string fileName, string fileUrl)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));
            if (string.IsNullOrWhiteSpace(dir)) throw new ArgumentNullException(nameof(dir));
            fileName = Legalize(fileName);
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));

            var filePath = Path.Combine(dir, fileName);

            try
            {
                using (var writer = new ProgressFileStream(filePath, false))
                {
                    var request = new RestRequest(fileUrl);
                    _client.DownloadStream(request).CopyTo(writer);
                }
            }
            catch (Exception ex)
            {
                File.Delete(filePath);
                throw new Exception($"文件下载失败！请重试\nUrl为：{fileUrl}", ex);
            }

            return filePath;
        }

        /// <summary>
        /// 使路径合法化，符合windows文件/文件夹命名规则
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string Legalize(string path)
        {
            path = path.Replace("\\", "")
               .Replace("/", "")
               .Replace(":", "")
               .Replace("*", "")
               .Replace("?", "")
               .Replace("\"", "")
               .Replace("<", "")
               .Replace(">", "")
               .Replace("|", "");
            return path;
        }
    }

    /// <summary>
    /// 后端报错引起的异常
    /// </summary>
    [Serializable]
    public class ApiException : Exception
    {
        public ApiException() { }
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception inner) : base(message, inner) { }
        protected ApiException(
         System.Runtime.Serialization.SerializationInfo info,
         System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
