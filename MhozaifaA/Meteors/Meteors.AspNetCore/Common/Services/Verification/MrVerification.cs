using Meteors.AspNetCore.Common.Services.Verification.Options;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.Verification
{
    public class MrVerification : IMrVerification
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly IDistributedCache distributedCache;
        private readonly IMemoryCache memoryCache;
        private readonly IDataProtector dataProtection;
        private readonly MrVerificationOptions options;

        public MrVerification(IDataProtectionProvider dataProtection, IHttpContextAccessor httpContext,
              IDistributedCache distributedCache = null, IMemoryCache memoryCache = null,
               IOptions<MrVerificationOptions> options = null)
        {
            this.httpContext = httpContext;
            this.distributedCache = distributedCache;
            this.memoryCache = memoryCache;
            this.options = options?.Value ?? new MrVerificationOptions();
            this.dataProtection = dataProtection.CreateProtector("dTvbat4#fsK832f-fmkdgGY#Rr^w");
        }

        private record IdPlain(string id, string plain);
        private string BaseOtpUrl => $"/{nameof(MrVerification)}";
        //{httpContext.HttpContext.Request.Scheme}://{httpContext.HttpContext.Request.Host}
        private string Key(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException($"{nameof(MrVerification)} Unique {nameof(id)} can't be null or empty");

            return $"{nameof(MrVerification)}:{id}";
        }


        private bool TryUnprotectUrl(string key, out string id, out string plain)
        {
            id = plain = String.Empty;
            try
            {
                var data = dataProtection.Unprotect(key);

                var obj = System.Text.Json.JsonSerializer.Deserialize<IdPlain>(data);
                id = obj.id;
                plain = obj.plain;
                return true;
            }
            catch (Exception ex) when (ex is CryptographicException || ex is RuntimeBinderException)
            {
                return false;
            }
        }

        public OtpVia Generate(string id)
        {
            return Generate(id, out _);
        }

        public OtpVia Generate(string id, out DateTime expire)
        {
            return Generate(id, options, out expire);
        }

        public OtpVia Generate(string id, MrVerificationOptions option)
        {
            return Generate(id, option, out _);
        }
        public OtpVia Generate(string id, int expireMin, out DateTime expire)
        {
            return Generate(id, new MrVerificationOptions()
            {
                IsInMemoryCache = options.IsInMemoryCache,
                EnableUrl = options.EnableUrl,
                UrlRoute =options.UrlRoute,
                Iterations = options.Iterations,
                Size = options.Size,
                Length = options.Length,
                Expire = expireMin,
            }, out expire);
        }

        public OtpVia Generate(string id, MrVerificationOptions option, out DateTime expire)
        {
            var plain = MrVerificationExtension.Generate(option, out expire, out string hash);

            if (options.IsInMemoryCache)
                memoryCache.Set(Key(id), hash, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = expire,
                    Priority = CacheItemPriority.High,
                });
            else
                distributedCache.SetString(Key(id), hash, new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = expire,
                });

            string url = string.Empty;
            if (option.EnableUrl)
                url = BaseOtpUrl+ option.UrlRoute + dataProtection.Protect(
                    System.Text.Json.JsonSerializer.Serialize(new IdPlain(id, plain)));

            return new OtpVia(plain, url);
        }


        public bool Scan(string id, string plain, MrVerificationOptions option)
        {
            string hash = string.Empty;

            if (options.IsInMemoryCache)
                hash = memoryCache.Get<string>(Key(id));
            else
                hash = distributedCache.GetString(Key(id));

            if (hash is null)
                return false;

            if (MrVerificationExtension.Scan(plain, hash, option))
            {
                if (options.IsInMemoryCache)
                    memoryCache.Remove(Key(id));
                else
                    distributedCache.Remove(Key(id));
                return true;
            }

            return false;
        }

        public bool Scan(string id, string plain, int expire)
        {
            return Scan(id, plain, new MrVerificationOptions() { Expire = expire });
        }

        public bool Scan(string id, string plain)
        {
            return Scan(id, plain, options);
        }

        public bool Scan(string url)
        {
            if (TryUnprotectUrl(url, out string id, out string code))
                return Scan(id, code);
            return false;
        }

        public bool Scan(string url,out string id, out string code)
        {
            return Scan(url, options.Expire, out id, out code);
        }

        public bool Scan(string url, int expire, out string id, out string code)
        {
            if (TryUnprotectUrl(url, out id, out code))
                return Scan(id, code, expire);
            return false;
        }
    }
}
