using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZuAnBot_WinForm
{
   static class ManifestResourceUtils
    {
        public static T GetJsonObject<T>(string jsonName)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"ZuAnBot_WinForm.Assets.{jsonName}");
            StreamReader reader = new StreamReader(stream);
            JsonSerializer serializer = new JsonSerializer();
            return (T)serializer.Deserialize(reader, typeof(T));
        }
    }
}
