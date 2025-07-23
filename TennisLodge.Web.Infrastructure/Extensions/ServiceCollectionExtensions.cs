using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using static TennisLodge.GCommon.ExceptionMessages;

namespace TennisLodge.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly string ServiceTypeSuffix = "Service";
        private static readonly string InterfacePrefix = "I";
        private static readonly string RepositoryTypeSuffix = "Repository";

        public static IServiceCollection AddUserDefineServices(this IServiceCollection serviceCollection, Assembly serviceAssembly)
        {
            Type[] serviceClasses = serviceAssembly
                .GetTypes()
                .Where(t => !t.IsInterface &&
                             t.Name.EndsWith(ServiceTypeSuffix))
                .ToArray();

            foreach (Type serviceClass in serviceClasses)
            {
                Type? serviceInterface = serviceClass
                    .GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"{InterfacePrefix}{serviceClass.Name}");
                if (serviceInterface == null)
                {
                    throw new ArgumentException(string.Format(InterfaceNotFoundMessage, serviceClass.Name));


                }

                serviceCollection.AddScoped(serviceInterface, serviceClass);

            }

            return serviceCollection;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection,
            Assembly repositoryAssembly)
        {
            Type[] repositoryClasses = repositoryAssembly
                .GetTypes()
                .Where(t => !t.IsInterface &&
                             !t.IsAbstract &&
                              t.Name.EndsWith(RepositoryTypeSuffix))
                .ToArray();

            foreach (Type repositoryClass in repositoryClasses)
            {
                Type? repositoryInterface = repositoryClass
                    .GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"{InterfacePrefix}{repositoryClass.Name}");

                if (repositoryInterface == null)
                {
                    throw new ArgumentException(string.Format(InterfaceNotFoundMessage, repositoryClass.Name));
                }
                serviceCollection.AddScoped(repositoryInterface, repositoryClass);
            }

            return serviceCollection;
        }
    }
}
