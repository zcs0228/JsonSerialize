using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonSerialize;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestClass testClass = new TestClass
            {
                Id = 1,
                Name = "zcs",
                Sex = true,
                Birthday = DateTime.Today.ToString()
            };

            string serializeResult = JsonHelper.ObjectToJson(testClass);
            Console.WriteLine(serializeResult);
            Console.ReadKey();

            TestClass deserializeResult = (TestClass)JsonHelper.JsonToObject(serializeResult, new TestClass());
            Console.WriteLine("Id: " + deserializeResult.Id);
            Console.WriteLine("Name: " + deserializeResult.Name);
            Console.WriteLine("Sex: " + deserializeResult.Sex);
            Console.WriteLine("Brithday: " + deserializeResult.Birthday);
            Console.ReadKey();
        }
    }
}
