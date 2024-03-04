using Meteors.AspNetCore.Domain.ConfigureServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Session.DependencyInjection
{
    public static class MrSessionMiddlewareExtensions
    {
        /// <summary>
        /// <para> <see cref="SupportedSessions.AspNet"/> : should use <see cref="MrSessionMiddlewareExtensions.UseMrSession"/> </para>
        /// <para> <see cref="SupportedSessions.Protect"/> : no need for Middleware</para>
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMrSession(this IApplicationBuilder app)
        {
            var option = app.ApplicationServices.GetService<IOptions<MrSessionOptions>>();
            if(option?.Value?.SupportedSession == SupportedSessions.AspNet)
                app.UseSession();
            return app;
        }

        //public static IApplicationBuilder UseMrSession(this IApplicationBuilder app, SessionOptions options)
        //{
        //    return app.UseSession(options);
        //}
    }

    public static class MrSessionServiceCollectionExtensions
    {
        /// <summary>
        /// AspCore session this will remove when (AspCore, Protect, Unsafe, Page) ,
        /// <para>Default <see cref="MrSessionOptions.Timeout"/> 20 minutes </para>
        /// <para>With : </para>
        /// <para> <see cref="SupportedSessions.AspNet"/> : should use <see cref="MrSessionMiddlewareExtensions.UseMrSession"/> </para>
        /// <para> <see cref="SupportedSessions.Protect"/> : no need for Middleware</para>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddMrSession(this IServiceCollection services, Action<MrSessionOptions> options)
        {
            services.Configure(options);

            MrSessionOptions _options = new();
            options(_options);

            switch (_options.SupportedSession)
            {
                case SupportedSessions.AspNet:
                    AspNetSessionServiceCollections(services, _options);
                    break;
                case SupportedSessions.Protect:
                    AspNetSessionServiceCollections(services, _options);
                    break;
                case SupportedSessions.Unsafe:

                    break;
                case SupportedSessions.Page:

                    break;
            }

            return services;
        }


        private static void AspNetSessionServiceCollections(IServiceCollection services, MrSessionOptions _options)
        {
            services.AddDistributedMemoryCache();
            // this add serverDataProtecion
            services.AddSession(options =>
            {
                options.Cookie.Name = _options.BackSessionName;
                options.IdleTimeout = _options.Timeout;
                options.Cookie.IsEssential = true;
            });
            services.TryAddMrHttpResolverService();
            services.AddTransient<IMrSession, MrSession>();
        }
    }
}
