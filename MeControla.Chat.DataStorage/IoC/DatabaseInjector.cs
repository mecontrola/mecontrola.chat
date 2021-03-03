using MeControla.Chat.DataStorage.Repositories;
using MeControla.Core.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace MeControla.Chat.DataStorage.IoC
{
    public class DatabaseInjector : IInjector
    {
        private const string DATABASE_NAME = "LocalDB";

        public void RegisterServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddDbContext<DbAppContext>(opt =>
            {
                opt.UseInMemoryDatabase(databaseName: DATABASE_NAME);
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.TryAddScoped<IDbAppContext, DbAppContext>();

            services.TryAddTransient<IRoomRepository, RoomRepository>();
            services.TryAddTransient<IUserRepository, UserRepository>();
        }
    }
}