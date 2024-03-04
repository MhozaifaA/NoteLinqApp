using Meteors.AspNetCore.Common.Services.Verification.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.Verification.DependencyInjection
{
    public static class MrVerificationServiceCollectionExtensions
    {
       
        public static IServiceCollection AddMrVerification(this IServiceCollection services, Action<MrVerificationOptions> options = default)
        {
            services.AddDataProtection();
            services.AddHttpContextAccessor();
            services.Add(ServiceDescriptor.Singleton<IMrVerification, MrVerification>());

            services.Configure(options ??= (o => { }));
            MrVerificationOptions _options = new();
            options(_options);

            if (_options.IsInMemoryCache)
                return services.AddMemoryCache();

            return services.AddStackExchangeRedisCache(options => options.Configuration = "localhost");
        }

 
    }

    public static class EndpointRouteBuilderExtensions
    {
        public static void MapMrVerification(this IEndpointRouteBuilder endpoints)
        {
            //endpoints.MapGet($"/{nameof(MrVerification)}/{{*key}}",
            //   ( key) =>
            //   {
            //       var otp = endpoints.ServiceProvider.GetRequiredService<IMrVerification>();
            //       if (otp.Scan(key))
            //           return "Verify";
            //       return "Un-Verify";
            //   });
        }
    }
}
