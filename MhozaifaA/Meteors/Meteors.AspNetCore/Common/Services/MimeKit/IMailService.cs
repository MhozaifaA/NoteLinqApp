using Meteors.AspNetCore.Common.Services.MimeKit.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.MimeKit
{
    public interface IMailService
    {
        void SendEmail(MailBody mailBody);
        Task SendEmailAsync(MailBody mailBody);
    }
}
