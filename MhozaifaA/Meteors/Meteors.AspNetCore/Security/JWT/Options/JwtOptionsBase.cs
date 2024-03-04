using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Security.JWT
{
    public class JwtOptionsBase
    {
        public const string Jwt = "jwt";

        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public long ExpireMinut { get; set; }

        protected void Configuration(JwtOptionsBase jwt)
        {
            Key= jwt.Key;
            Issuer= jwt.Issuer;
            Audience= jwt.Audience;
        }
    }
}
