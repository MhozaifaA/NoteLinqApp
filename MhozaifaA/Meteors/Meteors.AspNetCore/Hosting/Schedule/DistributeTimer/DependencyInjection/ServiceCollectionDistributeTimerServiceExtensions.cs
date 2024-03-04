using Meteors.AspNetCore.Helper.ExtensionMethods.DependencyInjection;
using Meteors.AspNetCore.Hosting.Schedule.DistributeTimer.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Hosting.Schedule.DistributeTimer.DependencyInjection
{
    //and middelware
    public static class ServiceCollectionAndBuilderDistributeTimerServiceExtensions
    {
        public static IServiceCollection AddMrDistributeTimer(this IServiceCollection services)
        {
            return services.AddSingleton<IMrDistributeTimer, MrDistributeTimer>();
        }

        public static IServiceCollection AddMrDistributeTimer(this IServiceCollection services, Action<MrDivDistributeTimerOption> option)
        {
            services.Configure(option);
            return services.AddMrDistributeTimer();
        }



        public static IApplicationBuilder UseMrDistributeTimer(this IApplicationBuilder app, Action<IMrDistributeTimer> action)
        {
            using (var ServiceScope = app.CreateFactoryScope())
            {
                action(ServiceScope.GetProviderService<IMrDistributeTimer>());
            }
            return app;
        }

        public static IApplicationBuilder UseMrDistributeTimer(this IApplicationBuilder app, Action<IMrDistributeTimer, IServiceProvider> action)
        {
            var ServiceScope = app.CreateFactoryScope();
            action(ServiceScope.GetProviderService<IMrDistributeTimer>(), ServiceScope.ServiceProvider);
            return app;
        }

        public static IApplicationBuilder UseMrDistributeTimer<TDbContext>(this IApplicationBuilder app, Action<IMrDistributeTimer, TDbContext> action) where TDbContext : DbContext
        {
            var ServiceScope = app.CreateFactoryScope();
            action(ServiceScope.GetProviderService<IMrDistributeTimer>(), ServiceScope.GetProviderService<TDbContext>());
            return app;
        }
    }
}
