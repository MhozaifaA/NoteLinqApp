using Meteors.AspNetCore.Common.Services.MimeKit;
using Meteors.AspNetCore.Common.Services.MimeKit.Options;
using Meteors.AspNetCore.Helper.ExtensionMethods.Boolean;
using Meteors.AspNetCore.Helper.ExtensionMethods.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Filter.Options
{
    public static class MrExceptionFilterOptionsExtensions
    {
        public static MrExceptionFilterOptions EnableLoggingToEmail(this MrExceptionFilterOptions options)
        {
            options.EnableLoggingToEmail = true;
            return options;
        }

        public static MrExceptionFilterOptions LoggingEmail(this MrExceptionFilterOptions options , LoggingEmail body)
        {
            options.LoggingEmail = body;
            options.EnableLoggingToEmail = options.LoggingEmail.ToEmail.IsNullOrEmpty().NestedIF(false, options.EnableLoggingToEmail);
            return options;
        }

        public static MrExceptionFilterOptions LoggingEmail(this MrExceptionFilterOptions options, string toEmail , string subject = default)
        {
            return options.LoggingEmail(new LoggingEmail(toEmail, subject));
        }

        public static MrExceptionFilterOptions UseMailOption(this MrExceptionFilterOptions options, Action<MailOptions> mailOptions)
        {
            options.MailOptions = mailOptions;
            return options;
        }
    }
}
