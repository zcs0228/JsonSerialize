using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JsonSerialize
{
    public static class JsonHelper
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
        /// Dictionary<int, JsonData>字典集合序列化为Json串，其中字典键值从零开始
        /// </summary>
        /// <param name="dictionarys"></param>
        /// <returns></returns>
        public static string DictionarysToJson(IEnumerable<Dictionary<int, JsonData>> dictionarys)
        {
            if (dictionarys.Count() == 0)
                return "[]";

            if (dictionarys.Count() == 1)
                return TranslateHelper.TranslateDictionary(dictionarys.FirstOrDefault());

            StringBuilder result = new StringBuilder();
            result.Append("[");

            foreach (var item in dictionarys)
            {
                string dataItem = TranslateHelper.TranslateDictionary(item);
                dataItem.Remove(0, 1);
                dataItem.Remove(dataItem.Length - 1, 1);
                result.Append(dataItem).Append(",");
            }
            result.Remove(result.Length - 1, 1);
            result.Append("]");

            return result.ToString();
        }
        /// <summary>
        /// 可定义名称的Dictionary序列化为Json
        /// </summary>
        /// <param name="jsonName"></param>
        /// <param name="dictionarys"></param>
        /// <returns></returns>
        public static string DictionarysToJson(string jsonName, IEnumerable<Dictionary<int, JsonData>> dictionarys)
        {
            StringBuilder result = new StringBuilder();
            result.Append("{\"").Append(jsonName).Append("\":");
            string jsons = DictionarysToJson(dictionarys);
            result.Append(jsons).Append("}");

            return result.ToString();
        }
        /// <summary>
        /// 转置table序列化Json
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static string TransformTableToJson(DataTable dataTable)
        {
            IList<Dictionary<int, JsonData>> resultObject = new List<Dictionary<int, JsonData>>();

            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                int i = 0;
                Dictionary<int, JsonData> columnsJson = new Dictionary<int, JsonData>();
                JsonData header = new JsonData();
                header.Name = "RowId";
                header.Value = dataTable.Columns[j].ColumnName;
                columnsJson.Add(i, header);
                i++;
                foreach (DataRow row in dataTable.Rows)
                {
                    JsonData dataItem = new JsonData();
                    dataItem.Name = "Col" + i.ToString();
                    dataItem.Value = row[j].ToString().Trim();
                    columnsJson.Add(i, dataItem);
                    i++;
                }

                resultObject.Add(columnsJson);
            }

            string result = DictionarysToJson(resultObject);

            return result;
        }
        /// <summary>
        /// json 字段值选择器
        /// </summary>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string JsonPick(this string json, string field)
        {
            return TranslateHelper.ValuePicker(field, json);
        }
        /// <summary>
        /// json字段值数组选择器
        /// </summary>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string[] JsonPickArray(this string json, string field)
        {
            return TranslateHelper.ArrayPicker(field, json);
        }
    }
}
