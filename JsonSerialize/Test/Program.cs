using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonSerialize;
using System.Data;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestClass testClass = new TestClass
            //{
            //    Id = 1,
            //    Name = "zcs",
            //    Sex = true,
            //    Birthday = DateTime.Today.ToString()
            //};

            //string serializeResult = JsonHelper.ObjectToJson(testClass);
            //Console.WriteLine(serializeResult);
            //Console.ReadKey();

            //TestClass deserializeResult = (TestClass)JsonHelper.JsonToObject(serializeResult, new TestClass());
            //Console.WriteLine("Id: " + deserializeResult.Id);
            //Console.WriteLine("Name: " + deserializeResult.Name);
            //Console.WriteLine("Sex: " + deserializeResult.Sex);
            //Console.WriteLine("Brithday: " + deserializeResult.Birthday);
            //Console.ReadKey();

            //JsonData data = new JsonData();
            //data.Name = "a";
            //data.Value = "b";
            //Dictionary<int, JsonData> dictionary = new Dictionary<int, JsonData>();
            //dictionary.Add(0, data);
            //dictionary.Add(1, data);
            //string result = JsonHelper.DictionaryToJson(dictionary);
            //Console.WriteLine(result);
            //Console.ReadKey();


            //JsonData data = new JsonData();
            //data.Name = "a";
            //data.Value = "b";
            //Dictionary<int, JsonData> dictionary = new Dictionary<int, JsonData>();
            //dictionary.Add(0, data);
            //dictionary.Add(1, data);
            //IList<Dictionary<int,JsonData>> listItems = new List<Dictionary<int, JsonData>>();
            //listItems.Add(dictionary);
            //listItems.Add(dictionary);
            //listItems.Add(dictionary);
            //string result = JsonHelper.DictionarysToJson("myJson", listItems);


            DataTable dt = new DataTable();
            dt.Columns.Add("a", typeof(int));
            dt.Columns.Add("b", typeof(string));
            dt.Rows.Add(1, "aaa");
            dt.Rows.Add(2, "bbb");

            //string result = JsonHelper.TransformTableToJson(dt);

            string result = DataTable2ChartJson.DataTable2ChartTable(dt, "tain", "dun", "200");
            
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
