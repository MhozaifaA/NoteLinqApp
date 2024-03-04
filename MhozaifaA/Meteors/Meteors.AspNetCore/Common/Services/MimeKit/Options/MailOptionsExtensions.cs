using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.MimeKit.Options
{
    public static class MailOptionsExtensions
    {
        public static MailOptions SetMail(this MailOptions options,string mail)
        {
            options.Mail = mail;
            return options;
        }

        public static MailOptions SetPassword(this MailOptions options, string password)
        {
            options.Password = password;
            return options;
        }

        public static MailOptions SetHost(this MailOptions options, string host)
        {
            options.Host = host;
            return options;
        }

        public static MailOptions SetPort(this MailOptions options, int port)
        {
            options.Port = port;
            return options;
        }
    }
}
