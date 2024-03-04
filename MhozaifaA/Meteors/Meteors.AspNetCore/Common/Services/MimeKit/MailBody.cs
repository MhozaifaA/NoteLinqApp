using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.MimeKit
{
    public class MailBody
    {
        public MailBody() { }

       
        public MailBody(string subject = default, string body = default, IFormFileCollection attachments = default, MessagePriority priority = MessagePriority.Normal, params string[] toEmail)
        {
            ToEmails = toEmail;
            Subject = subject;
            Body = body;
            Attachments = attachments;
            Priority= priority;
        }


        public string[] ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public MessagePriority Priority { get; set; } = MessagePriority.Normal;
        public IFormFileCollection Attachments { get; set; }
    }
}
