using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Security.JWT.DependencyInjection
{
    public static class JwtBearerAuthenticationServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services,Action<JwtBearerAuthenticationOptions> options)
        {
            services.Configure(options);
            JwtBearerAuthenticationOptions invokeOptions = new();
            options.Invoke(invokeOptions);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        RequireExpirationTime = true,
                        ValidIssuer = invokeOptions.Issuer,
                        ValidAudience = invokeOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(invokeOptions.Key))
                    };
                });
            return services.AddTransient<IJwtBearerAuthentication, JwtBearerAuthentication>();
        }

        public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services,JwtBearerAuthenticationOptions options)
        {
           return services.AddJwtBearerAuthentication((_options) => { _options = options; });
        }

        public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            var options = Configuration.GetSection(JwtOptionsBase.Jwt).Get<JwtBearerAuthenticationOptions>();
            return services.AddJwtBearerAuthentication(options);
        }

    }
}
