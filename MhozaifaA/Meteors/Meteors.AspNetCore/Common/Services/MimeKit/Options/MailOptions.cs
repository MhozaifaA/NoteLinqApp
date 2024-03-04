using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.MimeKit.Options
{
    public class MailOptions
    {
        internal string Mail { get; set; }
        /// <summary>
        /// If null mean 
        /// </summary>
        internal string? Password { get; set; }
        internal string Host { get; set; }
        internal int Port { get; set; }
    }
}
