using Meteors.AspNetCore.Localization.Translation.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Localization.Translation.DependencyInjection
{
    public static class MrTranslateServiceCollectionExtensions
    {
        /// <summary>
        /// <para>
        /// Exception on build:
        /// </para>
        /// <para>
        /// The Application Default Credentials are not available. They are available if running in Google Compute Engine. Otherwise, the environment variable GOOGLE_APPLICATION_CREDENTIALS must be defined pointing to a file defining the credentials. See https://developers.google.com/accounts/docs/application-default-credentials for more information.
        /// </para>
        /// </summary>
        /// <returns></returns>
        [Obsolete("Comment this Use when run Add-Migration")]
        public  static IServiceCollection AddMrTranslate(this IServiceCollection services,Action<MrTranslateOptions> options)
        {
            services.Configure(options);
            services.AddSingleton<IMrTranslate, MrTranslate>();
            return services;
        }
    }
}
