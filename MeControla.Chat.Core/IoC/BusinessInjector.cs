using MeControla.Chat.Core.Business;
using MeControla.Chat.Core.Commands;
using MeControla.Chat.Core.Executor;
using MeControla.Core.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace MeControla.Chat.Core.IoC
{
    public class BusinessInjector : IInjector
    {
        public void RegisterServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddTransient<IRoomBusiness, RoomBusiness>();
            services.TryAddTransient<ICommandFactory, CommandFactory>();
            services.TryAddTransient<IConnectExecutor, ConnectExecutor>();
            services.TryAddTransient<IResponseExecutor, ResponseExecutor>();
        }
    }
}