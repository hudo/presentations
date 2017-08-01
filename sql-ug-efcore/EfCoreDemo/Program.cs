using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EfCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            
            var db = new ConferenceContext();
            db.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());

            db.Database.EnsureCreated();

            Seed(db);

            Dump(db.Talks.Include(x => x.Presenter).ToList());

            var conf = db.Conferences.First();
            db.Entry(conf).Collection(x => x.Talks).Load();
            Dump(conf);

            Dump(db.Talks.Select(x => new
            {
                x.Id,
                Presenter = x.Presenter.Name,
                Title = x.Conference.Title,
            }));



            Console.WriteLine("done.");
        }


        static void Seed(ConferenceContext db)
        {
            if (!db.Conferences.Any())
            {
                db.Conferences.Add(new Conference
                {
                    Title = "Sql Ug Dublin",
                    Talks = new List<Talk>
                    {
                        new TechnicalTalk()
                        {
                            Title = "first talk",
                            Level = 200,
                            Presenter = new Presenter {Name = "presenter 1"}
                        },
                        new BusinessTalk()
                        {
                            Title = "second talk",
                            Topic = "PM",
                            Presenter = new Presenter {Name = "presenter 2"}
                        }
                    }
                });

                db.SaveChanges();
            }
        }

        static void Dump(object item)
        {
            var settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented, settings));
        }
    }
}