using MeControla.Chat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace MeControla.Chat.DataStorage.DataSeeding
{
    public static class MigrationData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Uuid = Guid.NewGuid(), Name = "Global" },
                new Room { Id = 2, Uuid = Guid.NewGuid(), Name = "Devops" }
            );
        }
    }
}