using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Core
{
    public static class MeteorsPackageEndpointRouteBuilderExtensions
    {
        public static void MapMeteors(this IEndpointRouteBuilder endpoints)
        {
             endpoints.MapGet("/meteors",
                context =>
                {
                    return context.Response.WriteAsync
                    ($"<div  class='meteors'> <p> Meteors.AspNetCore {MeteoresPackage.Version} </p> </div>" +
                    $"<style> " +
                    $".meteors {{" +
                    $"font-size: 500%;" +
                    $"display: flex;" +
                    $"font-family: monospace;" +
                    $"justify-content: center;" +
                    $"align-items: center;" +
                    $"height: 100%;" +
                    $"background: linear-gradient(120deg, rgba(255,255,255,1) 50%, rgba(0,0,0,1) 100%);" +
                    $"}}" +
                    $".meteors p{{" +
                    $"white-space: nowrap;"+
                    $"background: -webkit-linear-gradient(120deg, rgba(255,255,255,1) 50%, rgba(0,0,0,1) 100%);" +
                    $"-webkit-text-stroke: 0.2px gray;" +
                    $"-webkit-background-clip:text;" +
                    $"-webkit-text-fill-color: transparent;" +
                    $"}}" +
                    $" </style> ");
                });
        }
    }
  
    public class MeteoresPackage
    {
        public static string Version => Assembly.GetAssembly(typeof(MeteoresPackage)).GetName().Version.ToString();
    }
}
