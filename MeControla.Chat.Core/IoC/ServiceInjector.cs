using MeControla.Chat.Core.Services;
using MeControla.Core.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace MeControla.Chat.Core.IoC
{
    public class ServiceInjector : IInjector
    {
        public void RegisterServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddTransient<IRoomService, RoomService>();
            services.TryAddTransient<IUserService, UserService>();
        }
    }
}