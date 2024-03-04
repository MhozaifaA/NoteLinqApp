using Meteors.AspNetCore.Common.AuxiliaryImplemental.Classes;
using Meteors.AspNetCore.Core.Secure;
using Meteors.AspNetCore.Helper.ExtensionMethods.Object;
using Meteors.AspNetCore.Helper.ExtensionMethods.String;
using Meteors.AspNetCore.MVC.Resolver;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Session
{
    public class MrSession : IMrSession
    {
        private readonly IHttpResolverService httpResolver;
        private readonly IDataProtector dataProtection;
        private readonly MrSessionOptions options;

        public MrSession(IHttpResolverService httpResolver, IDataProtectionProvider dataProtection,
            IOptions<MrSessionOptions>  options)
        {
            this.httpResolver = httpResolver;
            this.dataProtection = dataProtection.CreateProtector(MeteorsSecure.Meteors_Protect_Session);
            this.options = options.Value;
        }

        public TObject? Get<TObject>(string key)
        {
            string value = null;
            switch (options.SupportedSession)
            {
                case SupportedSessions.AspNet:

                    value = httpResolver.HttpContext.Session.GetString(key);

                    break;
                case SupportedSessions.Protect:

                    var session = httpResolver.HttpContext.Request.Cookies[key];
                    if(session is not null)
                        value = options.Gzip ?
                            Compressor.DecompressFromBase64(dataProtection.Unprotect(session))
                            : dataProtection.Unprotect(session);

                    break;
                case SupportedSessions.Unsafe:
                    //here will be key from Server-Side
                    break;
                case SupportedSessions.Page:
                    //With Temporary data
                    break;
            }

            if (value is not null)
              return value.Deserialize<TObject>();

            return default(TObject?);
        }

        public TObject? Get<TObject>()
        {
            return Get<TObject>(options.BackSessionName);
        }

        public void Set<TObject>(string key, TObject value)
        {
            //if (value is null)
            //    throw new ArgumentException($"value session {typeof(TObject).Name} can't be null");

            var serializeValue = value?.Serialize()??string.Empty;

            switch (options.SupportedSession)
            {
                case SupportedSessions.AspNet:

                    httpResolver.HttpContext.Session.SetString(key, serializeValue);

                    break;
                case SupportedSessions.Protect:

                    httpResolver.HttpContext.Response.Cookies.Append(key: key,

                       value: dataProtection.Protect(
                           options.Gzip?
                           Compressor.CompressToBase64(serializeValue) : serializeValue),

                       new CookieOptions()
                       {
                           HttpOnly = true,
                           IsEssential = true,
                           Expires = DateTime.Now.AddTicks(options.Timeout.Ticks),
                       });

                    break;
                case SupportedSessions.Unsafe:

                    break;
                case SupportedSessions.Page:

                    break;
            }
        }

        public void Set<TObject>(TObject value)
        {
            Set(options.BackSessionName, value);
        }

    }
}
