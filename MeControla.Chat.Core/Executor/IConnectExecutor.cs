using MeControla.Chat.Core.Commands;
using System.Threading.Tasks;

namespace MeControla.Chat.Core.Executor
{
    public interface IConnectExecutor
    {
        Task<IResult> Execute(string connectionId, ICommand command);
    }
}