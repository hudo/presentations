using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.FullFx
{
    class Program
    {
        static void Main(string[] args)
        {
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();

            var db = new ConferenceContext();

            var conferences = db.Conferences.ToList();

            Console.WriteLine(conferences.Count);

            Console.WriteLine("done.");
        }
    }
}
