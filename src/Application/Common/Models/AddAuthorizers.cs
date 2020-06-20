using EventsCore.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EventsCore.Application.Common.Models
{
    /// <summary>
    /// Static class that adds authorization handlers from the assembly.
    /// </summary>
    public static class AddAuthorizers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <param name="lifetime"></param>
        public static void AddAuthorizersFromAssembly(
                this IServiceCollection services,
                Assembly assembly,
                ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var authorizerType = typeof(IAuthorizer<>);
            assembly.GetTypesAssignableTo(authorizerType).ForEach((type) =>
            {
                foreach (var implementedInterface in type.ImplementedInterfaces)
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(implementedInterface, type);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(implementedInterface, type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(implementedInterface, type);
                            break;
                    }
                }
            });
        }
        /// <summary>
        /// Retrieves the types.
        /// </summary>
        /// <param name="assembly">The assembly containing the authorization handlers</param>
        /// <param name="compareType">The type being compared.</param>
        /// <returns></returns>
        public static List<TypeInfo> GetTypesAssignableTo(this Assembly assembly, Type compareType)
        {
            var typeInfoList = assembly.DefinedTypes.Where(x => x.IsClass
                                && !x.IsAbstract
                                && x != compareType
                                && x.GetInterfaces()
                                        .Any(i => i.IsGenericType
                                                && i.GetGenericTypeDefinition() == compareType))?.ToList();

            return typeInfoList;
        }
    }
}
