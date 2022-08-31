using Domain;
using Infrasctruture.Mapping;
using Microsoft.EntityFrameworkCore;


namespace Infrasctruture.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options) {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProducerDTO>(new ProducerMap().Configure);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemory");
        }

        public DbSet<ProducerDTO> Producer { get; set; }


       
    }
}
