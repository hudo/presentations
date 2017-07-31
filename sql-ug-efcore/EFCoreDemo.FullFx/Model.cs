using System.Collections.Generic;
using System.Data.Entity;

namespace EFCoreDemo.FullFx
{
    public class ConferenceContext : DbContext
    {
        public ConferenceContext()
        {

        }

       

        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Talk> Talks { get; set; }
        public DbSet<Presenter> Presenters { get; set; }

        
    }

    public class Conference
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Talk> Talks { get; set; }
    }

    public class Talk
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Presenter Presenter { get; set; }
        public Conference Conference { get; set; }
    }

    public class Presenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
