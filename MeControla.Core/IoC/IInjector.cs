using Microsoft.Extensions.DependencyInjection;

namespace MeControla.Core.IoC
{
    public interface IInjector
    {
        void RegisterServices(IServiceCollection services);
    }
}