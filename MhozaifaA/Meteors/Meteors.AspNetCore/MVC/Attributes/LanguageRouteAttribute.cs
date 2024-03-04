using Meteors.AspNetCore.MVC.Route;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class LanguageRouteAttribute : RouteAttribute
    {
        public LanguageRouteAttribute() : base(MrRoute.LanguageRoute) { }
    }
}
