using Meteors.AspNetCore.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Route
{
    /// <summary>
    /// General class contain namespace route
    /// </summary>
    public class MrRoute
    {

        public const string ControllerActionRoute = "[" + FixedCommonValue.Controller + "]/[" + FixedCommonValue.Action + "]";

        /// <summary>
        /// ~/
        /// </summary>
        public const string EmptyRoute = FixedCommonValue.Tilde + FixedCommonValue.Slash; 
        /// <summary>
        /// ~/api/
        /// </summary>
        public const string EmptyApiRoute = MrRoute.EmptyRoute + FixedCommonValue.API + "/";
        /// <summary>
        /// ~/api/[controller]/[action]
        /// </summary>
        public const string DefaultRoute = FixedCommonValue.Tilde + "/"+ FixedCommonValue.API + "/"+ MrRoute.ControllerActionRoute;
        /// <summary>
        /// ~/api/{{lang}}/[controller]/[action]
        /// </summary>
        public const string LanguageRoute = FixedCommonValue.Tilde + "/" + FixedCommonValue.API + "/{" + FixedCommonValue.Lang + "}/" + MrRoute.ControllerActionRoute;

    }
}
