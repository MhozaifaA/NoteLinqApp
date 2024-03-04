using Meteors.AspNetCore.IO.Files;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Meteors.AspNetCore.IO.Files
{
     public static class MrFillServiceCollectionExtensions
    {
       
        public static IServiceCollection AddMrFileService(this IServiceCollection Service, Action<MrFileOption> option = null )
        {

            (option != null ? Service.Configure(option) : Service )
                                     .AddTransient<IMrFileService, MrFileService>();

            return Service;
        }
    }
}
