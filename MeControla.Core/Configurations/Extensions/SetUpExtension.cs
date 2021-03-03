using MeControla.Core.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MeControla.Core.Configurations.Extensions
{
    public static class SetUpExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var injectors = LoadAssemblies<IInjector>().ToList();
            injectors.ForEach(installer => installer.RegisterServices(services));
        }

        private static IEnumerable<T> LoadAssemblies<T>()
            => LoadAppAssemblies().Select(itm => itm.ExportedTypes
                                                    .Where(x => typeof(T).IsAssignableFrom(x)
                                                             && !x.IsInterface
                                                             && !x.IsAbstract)
                                                    .Select(Activator.CreateInstance)
                                                    .Cast<T>())
                                  .SelectMany(x => x)
                                  .ToList();

        private static IEnumerable<Assembly> LoadAppAssemblies()
            => new DirectoryInfo(GetAppBaseDirectory()).GetFiles("*.dll", SearchOption.TopDirectoryOnly)
                                                       .Select(itm =>
                                                       {
                                                           var assemblyName = AssemblyName.GetAssemblyName(itm.FullName);
                                                           try
                                                           {
                                                               return AppDomain.CurrentDomain.Load(assemblyName);
                                                           }catch
                                                           {
                                                               return null;
                                                           }
                                                       })
                                                       .Where(itm => itm != null);

        private static string GetAppBaseDirectory()
            => AppDomain.CurrentDomain.BaseDirectory;
    }
}