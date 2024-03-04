using Meteors.AspNetCore.Common.Services.MimeKit.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.MimeKit.DependencyInjection
{
    public static class MrMailServiceCollectionExtensions
    {
        public static IServiceCollection AddMrMail(this IServiceCollection services)
        {
            return services.AddScoped<IMailService , MailService>();
        }

        public static IServiceCollection AddMrMail(this IServiceCollection services,Action<MailOptions> option)
        {
            services.Configure(option);
            return services.AddMrMail();
        }
    }
}
