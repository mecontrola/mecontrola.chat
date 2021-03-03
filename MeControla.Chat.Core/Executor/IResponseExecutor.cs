using System.Collections.Generic;

namespace MeControla.Chat.Core.Executor
{
    public interface IResponseExecutor
    {
        IList<MessageResult> Generate(IResult result);
    }
}