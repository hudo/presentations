using System;
using Newtonsoft.Json;

namespace EfCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        static void Dump(object item)
        {
            var settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented, settings));
        }
    }
}