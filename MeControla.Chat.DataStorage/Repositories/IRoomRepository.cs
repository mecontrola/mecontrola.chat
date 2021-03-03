using MeControla.Chat.Data.Entities;
using MeControla.Core.Repositories;
using System.Threading.Tasks;

namespace MeControla.Chat.DataStorage.Repositories
{
    public interface IRoomRepository : IAsyncRepository<Room>
    {
        Task<Room> FindBydNameAsync(string name);
    }
}