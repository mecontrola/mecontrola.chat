using MeControla.Chat.Data.Entities;
using MeControla.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MeControla.Chat.DataStorage
{
    public interface IDbAppContext : IDbContext
    {
        DbSet<Room> Rooms { get; }
        //DbSet<RoomUser> RoomUsers { get; }
        DbSet<User> Users { get; }
    }
}