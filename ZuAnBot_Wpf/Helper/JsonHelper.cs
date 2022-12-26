using Newtonsoft.Json;
using System.IO;
using System.Linq;
using ZuAnBot_Wpf.ViewModels;

namespace ZuAnBot_Wpf.Helper
{
    /// <summary>
    /// json持久化工具类
    /// </summary>
    public static class JsonHelper
    {
        public static T DeserializeManifestJson<T>(string jsonName)
        {
            var stream = ManifestHelper.GetManifestStream(jsonName);

            return DeserializeStream<T>(stream);
        }

        /// <summary>
        /// 反序列化词库
        /// </summary>
        /// <returns></returns>
        public static WordsLibrary DeserializeWordsLibrary()
        {
            var stream = new FileStream(LocalConfigHelper.WordsLibraryPath,FileMode.Open) ;

            #region 给word的category导航属性赋值
            var libray = DeserializeStream<WordsLibrary>(stream);
            foreach (var category in libray.Categories)
            {
                category.TargetCategories = libray.Categories.Where(x => x != category).ToList();

                category.Library = libray;

                foreach (var word in category.Words)
                {
                    word.Category = category;
                }
            }

            #endregion 给word的category导航属性赋值

            return libray;
        }

        /// <summary>
        /// 序列化词库
        /// </summary>
        /// <param name="library"></param>
        public static void SerializeWordsLibrary(WordsLibrary library)
        {
            if (library == null) return;

            using (var streamWritter = File.CreateText(LocalConfigHelper.WordsLibraryPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(streamWritter, library);
            }
        }

        private static T DeserializeStream<T>(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            JsonSerializer serializer = new JsonSerializer();
            return (T)serializer.Deserialize(reader, typeof(T));
        }
    }
}
