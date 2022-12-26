using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuAnBot_Wpf.Helper
{
    public class Env
    {
        /// <summary>
        /// 环境名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UseApi { get; set; }
    }

    public class EnvConfigs
    {
        /// <summary>
        /// 当前环境
        /// </summary>
        public string Env { get; set; }

        /// <summary>
        /// 环境配置列表
        /// </summary>
        public List<Env> Envs { get; set; }
    }

    public static class EnvConfigHelper
    {
        private static readonly string JsonFile = "Url.json";

        public static Env Env
        {
            get
            {
                var configs = JsonHelper.DeserializeManifestJson<EnvConfigs>(JsonFile);

                return configs.Envs.FirstOrDefault(x => x.Name == configs.Env) ?? throw new Exception($"不存在配置环境：{configs.Env}");
            }
        }
    }
}
