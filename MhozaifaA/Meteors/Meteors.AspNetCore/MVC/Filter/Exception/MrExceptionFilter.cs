using Meteors.AspNetCore.Common.Services.MimeKit;
using Meteors.AspNetCore.Helper.ExtensionMethods.Exception;
using Meteors.AspNetCore.MVC.Filter.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Filter
{
    public class MrExceptionFilter : IAsyncExceptionFilter
    {
        private readonly MrExceptionFilterOptions options;
        private readonly IMailService mailServce;

        public MrExceptionFilter( IOptions<MrMvcFilterOptions> options, IMailService mailServce = null)
        {
            this.options = options?.Value.ExceptionFilter;
            this.options ??= new MrExceptionFilterOptions();
            this.mailServce = mailServce;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                string exString = $"Date : {DateTime.Now} {context.Exception.ToFullException()}";
                context.Result = new JsonResult(context.Exception.ToFullException()) { StatusCode = 500 };

                if (options.EnableLoggingToEmail)
                {
                    try
                    {
                     if(mailServce is not null)
                        await mailServce.SendEmailAsync(new MailBody(
                                             toEmail: options.LoggingEmail.ToEmail,
                                             subject: options.LoggingEmail.Subject,
                                             body: exString));
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"From {nameof(MrExceptionFilter)}: {e.ToFullException()}");
                    }
                }

                context.ExceptionHandled = true;
            }
        }
    }
}
