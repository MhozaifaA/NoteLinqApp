using Meteors.AspNetCore.MVC.Security.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meteors.AspNetCore.Helper.ExtensionMethods.Enum;
using Meteors.AspNetCore.Core.Shared;

namespace Meteors.AspNetCore.MVC.Security.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class MrAuthorizeAttribute : AuthorizeAttribute
    {
        public MrAuthorizeAttribute() { }
        public MrAuthorizeAttribute(params string[] roles) : this()
        {
            if (roles is not null && roles.Length != 0)
                base.Roles = string.Join(FixedCommonValue.Comma, roles);            
        }
        public MrAuthorizeAttribute(params MrRoles[] roles) : this(roles.ToStringArray()) { }

    }
}
