using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.ExpressionTreeProcessors;

namespace EfCoreDemo
{
    public class ConferenceContext : DbContext
    {
        public ConferenceContext()
        {
            
        }

        public ConferenceContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Talk> Talks { get; set; }
        public DbSet<Presenter> Presenters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessTalk>().HasBaseType<Talk>();
            modelBuilder.Entity<TechnicalTalk>().HasBaseType<Talk>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=conferences;Trusted_Connection=true");
        }
    }

    public class Conference
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Talk> Talks { get; set; }
    }

    public abstract class Talk
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Presenter Presenter { get; set; }
        public Conference Conference { get; set; }
    }

    public class TechnicalTalk : Talk
    {
        public int Level { get; set; }
    }

    public class BusinessTalk : Talk
    {
        public string Topic { get; set; }
    }


    public class Presenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
