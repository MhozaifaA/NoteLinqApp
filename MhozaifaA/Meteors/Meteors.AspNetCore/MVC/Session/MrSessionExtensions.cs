using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Session
{
    public static class elsessionExtensions
    {
        public static byte[] GetValueOrDefault(this ISession session,string key)
        {
            if (session.TryGetValue(key, out byte[] value))
                return value;
            return default;
        }
    }
}
