using MeControla.Chat.Core.Mappers.EntityToDto;
using MeControla.Core.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace MeControla.Chat.Core.IoC
{
    public class MapperInjector : IInjector
    {
        public void RegisterServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddSingleton<IRoomEntityToDtoMapper, RoomEntityToDtoMapper>();
            services.TryAddSingleton<IRoomUsersEntityToDtoMapper, RoomUsersEntityToDtoMapper>();
            services.TryAddSingleton<IUserEntityToDtoMapper, UserEntityToDtoMapper>();
        }
    }
}