using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace PersonalLauncher.Source
{
    public static class FileSystem
    {
        public static string LoadJson(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
            {
                return r.ReadToEnd();
            }
        }

        public static T GetConfigurationFromJson<T>(string json)
        {
            T myDeserializedClass = JsonConvert.DeserializeObject<T>(value: json);

            if (myDeserializedClass == null)
                return default(T);

            return myDeserializedClass;
        }
    }
}
