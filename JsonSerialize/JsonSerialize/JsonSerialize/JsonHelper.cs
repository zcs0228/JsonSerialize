using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerialize
{
    public class JsonHelper
    {
        /// <summary>
        /// 从一个对象信息生产Json串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
        }
        /// <summary>
        /// Json串反序列化为对象
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object JsonToObject(string jsonString, Object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return serializer.ReadObject(stream);
        }
        /// <summary>
        /// Dictionary<int, JsonData>转换为Json串, 键值从零开始
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static string DictionaryToJson(Dictionary<int, JsonData> dictionary)
        {
            StringBuilder result = new StringBuilder();
            int valueCount = dictionary.Count();

            if (valueCount == 0)
                return "[]";

            result.Append("[");

            for (int i = 0; i < valueCount; i++)
            {
                string item = String.Format("{\"{0}\":\"{1}\"", dictionary[i].Name, dictionary[i].Value);
                result.Append(item).Append(",");
            }
            result.Remove(result.Length - 1, 1);
            result.Append("]");

            return "";
        }
    }
}
