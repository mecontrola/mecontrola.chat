using MeControla.Chat.Data.Entities;
using MeControla.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeControla.Chat.DataStorage.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<IList<User>> GetAllFromRoom(long id);
        Task<User> FindByNameAsync(string name);
        Task<User> FindByConnectionIdAsync(string connectionId);
    }
}