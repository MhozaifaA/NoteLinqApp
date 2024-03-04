using Meteors.AspNetCore.Common.Services.MimeKit.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Filter.Options
{
    public class MrExceptionFilterOptions
    {
        internal bool EnableLoggingToEmail { get; set; } = false;
        internal LoggingEmail LoggingEmail { get; set; }
        internal Action<MailOptions> MailOptions { get; set; }
    }

    public class LoggingEmail
    {
        public LoggingEmail() { }

        public LoggingEmail(string toEmail, string subject=default)
        {
            ToEmail = toEmail;
            Subject = subject;
        }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
    }
}
