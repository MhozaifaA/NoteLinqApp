
using Meteors.AspNetCore.Infrastructure.JsonStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class JsonContextServicesCollectionsExtensions
    {
        public static IServiceCollection AddJsonContext<T>(this IServiceCollection services) where T : JsonContext
        {
            return services.AddScoped<T>();
        }
    }
}
