using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeControla.Core.IoC
{
    public interface IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration);
    }
}