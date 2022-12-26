using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZuAnBot_Wpf.Api;
using ZuAnBot_Wpf.Api.Dtos;

namespace ZuAnBot_Wpf.Helper
{
    public static class VersionHelper
    {
        private static readonly Apis _apis = Apis.GetInstance();
        private readonly static string _pattern = @"^[0-9]{1,2}\.[0-9]{1,2}\.[0-9]{1,2}$";

        /// <summary>
        /// 获取最新版本
        /// </summary>
        /// <returns></returns>
        public static async Task<VersionDto> GetLatestVersion()
        {
            try
            {
                return await _apis.GetLatestVersion();
            }
            catch (ApiException)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取当前版本名称
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentVersionName()
        {
            return System.Windows.Application.ResourceAssembly.GetName().Version.ToString(3);
        }

        /// <summary>
        /// 输入后端最新版本信息，判断当前版本是否是最新版本
        /// </summary>
        /// <param name="latestVersion"></param>
        /// <returns></returns>
        public static bool IsNewestVersion(VersionDto latestVersion)
        {
            if (latestVersion == null) return true;

            var currentVersionName = GetCurrentVersionName();

            return CompareVersion(currentVersionName, latestVersion.VersionName) >= 0;
        }

        /// <summary>
        /// 比较两个版本号
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns>v1小于v2则返回-1，v1等于v2返回0，v1大于v2返回1</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int CompareVersion(string v1, string v2)
        {
            if (!Regex.IsMatch(v1, _pattern) || !Regex.IsMatch(v2, _pattern))
            {
                throw new ArgumentOutOfRangeException("指定版本号格式无效");
            }

            var version1 = v1.Split('.').Select(int.Parse).ToArray();
            var version2 = v2.Split('.').Select(int.Parse).ToArray();

            for (int i = 0; i < 3; i++)
            {
                if (version1[i] > version2[i])
                    return 1;
                else if (version1[i] < version2[i])
                    return -1;
            }

            return 0;
        }
    }
}
