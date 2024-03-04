using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.FCM
{
    public static class MrFCMServiceCollectionExtensions
    {
        
        public static IServiceCollection AddMrFCM(this IServiceCollection services)
        {
            return services.AddHttpClient(MrFCMOptions.Name, c =>
                c.BaseAddress = new Uri("https://fcm.googleapis.com")).Services.
                AddSingleton<IMrFCMService, MrFCMService>();
        }
        
        public static IServiceCollection AddMrFCM(this IServiceCollection services, Action<MrFCMOptions> options)
        {
            return services.Configure(options).AddMrFCM();
        }
    }
}
