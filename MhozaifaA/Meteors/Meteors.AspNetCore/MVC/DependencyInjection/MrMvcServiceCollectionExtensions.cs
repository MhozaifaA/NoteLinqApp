using Meteors.AspNetCore.Common.Services.MimeKit.DependencyInjection;
using Meteors.AspNetCore.Common.Services.MimeKit.Options;
using Meteors.AspNetCore.MVC.Filter;
using Meteors.AspNetCore.MVC.Filter.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.DependencyInjection
{
    public static class MrMvcServiceCollectionExtensions
    {
        public static IMvcBuilder AddMrControllers(this IServiceCollection services)
        {
            return services.AddControllers(options => {
                options.Filters.Add(typeof(MrExceptionFilter));
            });
        }

        public static IMvcBuilder AddMrControllers(this IServiceCollection services , Action<MrMvcFilterOptions> option)
        {
            services.Configure(option);

            MrMvcFilterOptions filter = new();
            option(filter);

            if(filter.ExceptionFilter.EnableLoggingToEmail)
               services.AddMrMail(filter.ExceptionFilter.MailOptions);

            return services.AddControllers(options => {
                options.Filters.Add(typeof(MrExceptionFilter));
            });
        }

    }
}
