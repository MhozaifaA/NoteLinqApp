using Microsoft.Extensions.DependencyInjection;
using Meteors.AspNetCore.Service.BoundedContext.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Options;
using Meteors.AspNetCore.Service.Options;
using Meteors.AspNetCore.Infrastructure.ModelEntity.Interface;
using Meteors.AspNetCore.Helper.ExtensionMethods.Object;
using Meteors.AspNetCore.Core.Shared;

/// <summary>
/// ---------------------------------------------
/// | ::Mrkood:: AddMrRepositoryInject version 1.1.0 |
/// ---------------------------------------------
/// </summary>
namespace Meteors.AspNetCore.Service.DependencyInjection
{
    /// <summary>
    ///  Extension methods for add MrRepository Service
    /// </summary>
    public static class MrRepositoryServiceCollectionExtensions
    {

        /// <summary>
        /// Inject all repository wich used <see cref="MrRepositoryAttribute"/> with specific <see cref="Lifetimes"/> .
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyString">Loads an assembly given the long form of its name.</param>
        /// <exception cref="AmbiguousMatchException"></exception>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddMrRepositoryInject(this IServiceCollection services,params string[] assemblyString)
         => _AddMrRepsitoryInject(services, assemblyString);

        /// <summary>
        /// Inject all repository wich used <see cref="MrRepositoryAttribute"/> with specific <see cref="Lifetimes"/> .
        /// </summary>
        /// <param name="services"></param>
        /// <param name="anyClassInAssembly">Pass any time inside assemply</param>
        /// <exception cref="AmbiguousMatchException"></exception>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddMrRepositoryInject(this IServiceCollection services,params Type[] anyClassInAssembly)
             => _AddMrRepsitoryInject(services, anyClassInAssembly);


        /// <summary>
        /// Inject all repository wich used <see cref="MrRepositoryAttribute"/> with specific <see cref="Lifetimes"/> .
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Assembly">Represents an assembly</param>
        /// <exception cref="AmbiguousMatchException"></exception>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddMrRepositoryInject(this IServiceCollection services, params Assembly[] Assembly) 
            => _AddMrRepsitoryInject(services, Assembly);



        public static IServiceCollection AddMrRepository(this IServiceCollection services, Action<MrRepositoryOptions> options)
        {
            services.Configure(options);
            MrRepositoryOptions _options = new MrRepositoryOptions();
             options(_options);
            _AddMrRepsitoryInject(services, _options.Assemblies.ToArray());
            return services;
        }




        /// <summary>
        /// Generic array of <see langword="namespace"/> as <see cref="string"/> ,<see cref="Type"/> and <see cref="Assembly"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="any"></param>
        /// <returns></returns>
        private static IServiceCollection _AddMrRepsitoryInject<T>(IServiceCollection services, T[] any)
        {
            var Types = any.AbleNameSpaceToType();
            foreach (Type type in Types)
                InjectRepositories(services, type);
            return services;
        }

      

        /// <summary>
        /// Inject all <see cref="MrRepositoryAttribute"/> users by <see cref="Lifetimes"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        private static void InjectRepositories(IServiceCollection services, Type type)
        {
            if (Attribute.IsDefined(type, typeof(MrRepositoryAttribute)))
            {
                Type inheritanceInterfaceType = type.GetInterface(FixedCommonValue.I + type.Name);
                if (inheritanceInterfaceType is null)
                    throw new AmbiguousMatchException($"There is not match interface named {FixedCommonValue.I + type.Name} \n please check {type.Name} .");
                switch (MrRepositoryAttribute.GetLifetime(type))
                {
                    case Lifetimes.Singleton:
                        services.AddSingleton(inheritanceInterfaceType, type);
                        break;
                    case Lifetimes.Scoped:
                        services.AddScoped(inheritanceInterfaceType, type);
                        break;
                    case Lifetimes.Transient:
                        services.AddTransient(inheritanceInterfaceType, type);
                        break;
                }
            }

            //if(Attribute.IsDefined(type, typeof(MrStoreAttribute)))
            //{
            //    //var StoreClasses = type.GetNestedTypes().Where(x=> 
            //    //(x.GetInterface(nameof(_IFilter))!= null ) || (x.GetInterface(nameof(_IQuery))!=null));
            //    services.AddScoped(type);
            //    foreach (var storeClassType in type.BaseType.GenericTypeArguments)
            //    {
            //        services.AddScoped(storeClassType);
            //    }
            //}

        }
    }
}
