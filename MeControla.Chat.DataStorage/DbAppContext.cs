using MeControla.Chat.Data.Entities;
using MeControla.Chat.DataStorage.DataSeeding;
using Microsoft.EntityFrameworkCore;

namespace MeControla.Chat.DataStorage
{
    public class DbAppContext : DbContext, IDbAppContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }

        public DbAppContext(DbContextOptions<DbAppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbAppContext).Assembly);
            modelBuilder.Seed();
        }
    }
}