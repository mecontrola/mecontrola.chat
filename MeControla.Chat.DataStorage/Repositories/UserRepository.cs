using MeControla.Chat.Data.Entities;
using MeControla.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeControla.Chat.DataStorage.Repositories
{
    public class UserRepository : BaseAsyncRepository<User>, IUserRepository
    {
        public UserRepository(IDbAppContext context)
            : base(context, context.Users)
        { }

        public async Task<User> FindByConnectionIdAsync(string connectionId)
            => await dbSet.AsNoTracking()
                          .Include(itm => itm.Room)
                          .SingleOrDefaultAsync(itm => itm.ConnectionId.Equals(connectionId));

        public async Task<User> FindByNameAsync(string name)
            => await dbSet.AsNoTracking()
                          .Include(itm => itm.Room)
                          .SingleOrDefaultAsync(itm => itm.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        public async Task<IList<User>> GetAllFromRoom(long roomId)
            => await dbSet.AsNoTracking()
                          .Include(itm => itm.Room)
                          .Where(itm => itm.RoomId.Equals(roomId))
                          .ToListAsync();
    }
}