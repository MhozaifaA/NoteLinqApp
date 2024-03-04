using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.Verification.Options
{
    public static class MrVerificationOptionsExtensions
    {
        public static MrVerificationOptions UseInMemoryCache(this MrVerificationOptions options)
        {
            options.IsInMemoryCache = true;
            return options;
        }
    }

}
