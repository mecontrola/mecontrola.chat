using MeControla.Chat.Data.Entities;
using MeControla.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MeControla.Chat.DataStorage.Repositories
{
    public class RoomRepository : BaseAsyncRepository<Room>, IRoomRepository
    {
        public RoomRepository(IDbAppContext context)
            : base(context, context.Rooms)
        { }

        public async Task<Room> FindBydNameAsync(string name)
            => await dbSet.AsTracking()
                          .SingleOrDefaultAsync(itm => itm.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }
}