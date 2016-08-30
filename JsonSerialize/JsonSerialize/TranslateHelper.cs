using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JsonSerialize
{
    public class TranslateHelper
    {
        public static string TranslateJsonData(JsonData data)
        {
            string result = String.Format("\"{0}\":\"{1}\"", data.Name, data.Value);
            return result;
        }
        /// <summary>
        /// Dictionary<int, JsonData>转换为Json串, 键值从零开始
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static string TranslateDictionary(Dictionary<int, JsonData> dictionary)
        {
            StringBuilder result = new StringBuilder();
            int valueCount = dictionary.Count();

            if (valueCount == 0)
                return "{}";

            result.Append("{");

            for (int i = 0; i < valueCount; i++)
            {
                //result.Append("{\"").Append(dictionary[i].Name).Append("\":\"").Append(dictionary[i].Value).Append("\"}");
                //string item = String.Format("\"{0}\":\"{1}\",", dictionary[i].Name, dictionary[i].Value);
                string item = TranslateJsonData(dictionary[i]);
                result.Append(item).Append(",");
            }
            result.Remove(result.Length - 1, 1);
            result.Append("}");

            return result.ToString();
        }
        /// <summary>
        /// json 字段值选择器
        /// </summary>
        /// <param name="field"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        /// 
        public static string ValuePicker(string field, string json)
        {
            // \s.*? : 任意空白字符
            var rgSimpleValue = new Regex(@"""" + field + @""":""([^""""]+)""");
            var rgComplexValue = new Regex(@"""" + field + @""":((?x){[^{}]*(((?'k'{)[^{}]*)+((?'-k'})[^{}]*)+)*(?(k)(?!))})");
            var rgArrayValue = new Regex(@"""" + field + @""":((?x)\[[^\[\]]*(((?'k'{)[^\[\]]*)+((?'-k'})[^\[\]]*)+)*(?(k)(?!))\])");

            var mc = rgSimpleValue.Matches(json);

            if (mc.Count == 0)
            {
                mc = rgComplexValue.Matches(json);

                if (mc.Count == 0)
                {
                    mc = rgArrayValue.Matches(json);

                    if (mc.Count == 0)
                        return "";
                }
            }

            return mc[0].Groups[1].Value;
        }
        /// <summary>
        /// json字段值数组选择器
        /// </summary>
        /// <param name="field"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string[] ArrayPicker(string field, string json)
        {
            string arrayJson = ValuePicker(field, json);

            if (arrayJson.StartsWith("[") == false || arrayJson.EndsWith("]") == false)
                return new string[] { };

            IList<string> results = new List<string>();

            var rgElement = new Regex("(?x){[^{}]*(((?'k'{)[^{}]*)+((?'-k'})[^{}]*)+)*(?(k)(?!))}");

            var mc = rgElement.Matches(arrayJson);

            if (mc.Count == 0)
                return new string[] { };

            for (int i = 0; i < mc.Count; i++)
            {
                results.Add(mc[i].Value);
            }

            return results.ToArray();
        }
    }
}
