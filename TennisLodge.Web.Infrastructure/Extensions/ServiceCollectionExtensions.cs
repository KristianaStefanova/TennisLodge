using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Services.Core;
using TennisLodge.Services.Core.Interfaces;

namespace TennisLodge.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly string ServiceTypeSuffix = "Service";
        private static readonly string ServiceInterfacePrefix = "I";

        public static IServiceCollection AddUserDefineServices(this IServiceCollection serviceCollection, Assembly serviceAssembly)
        {
            Type[] serviceClasses = serviceAssembly
                .GetTypes()
                .Where(t => !t.IsInterface &&
                             t.Name.EndsWith(ServiceTypeSuffix))
                .ToArray();

            foreach (Type serviceClass in serviceClasses)
            {
                Type[] serviceClassInterfaces = serviceClass
                    .GetInterfaces();
                if (serviceClassInterfaces.Length == 1 &&
                    serviceClassInterfaces.First().Name.StartsWith(ServiceInterfacePrefix) &&
                    serviceClassInterfaces.First().Name.EndsWith(ServiceTypeSuffix))
                {
                    Type serviceClassInterface = serviceClassInterfaces.First();

                    serviceCollection.AddScoped(serviceClassInterface, serviceClass);
                }
            }

            return serviceCollection;
        }
    }
}
