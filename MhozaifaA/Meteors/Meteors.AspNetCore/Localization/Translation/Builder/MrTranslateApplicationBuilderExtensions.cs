using Microsoft.AspNetCore.Builder;
using System;

namespace Meteors.AspNetCore.Localization.Translation.Builder
{
    public static class MrTranslateApplicationBuilderExtensions
    {
        /// <summary>
        /// <para>
        /// Exception on build:
        /// </para>
        /// <para>
        /// The Application Default Credentials are not available. They are available if running in Google Compute Engine. Otherwise, the environment variable GOOGLE_APPLICATION_CREDENTIALS must be defined pointing to a file defining the credentials. See https://developers.google.com/accounts/docs/application-default-credentials for more information.
        /// </para>
        /// </summary>
        /// <param name="app"></param>
        /// <param name="credential_path"></param>
        /// <returns></returns>
        [Obsolete("Comment AddMrTransalte this Use when run Add-Migration")]
        public static IApplicationBuilder UseMrTranslate(this IApplicationBuilder app,string credential_path)
        {
            //https://codelabs.developers.google.com/codelabs/cloud-translation-csharp#5
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);

            return app;
        }

    }


}
