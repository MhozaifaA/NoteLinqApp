using Meteors.AspNetCore.Common.Services.MimeKit.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.MimeKit
{
    public class MailService : IMailService
    {
        private readonly MailOptions options;
        public MailService(IOptions<MailOptions> options)
        {
            this.options = options?.Value;
            this.options ??= new MailOptions();
        }

        public void SendEmail(MailBody mailBody) => SendEmail(options,mailBody);
        public async Task SendEmailAsync(MailBody mailBody) => await SendEmailAsync(options, mailBody);




        public static async Task SendEmailAsync(MailOptions options, MailBody mailBody)
        {
            if (mailBody.ToEmails == default || mailBody.ToEmails is null || mailBody.ToEmails.Length == 0)
                throw new ArgumentNullException($"{nameof(mailBody.ToEmails)} cant't  be null or empty");

            MimeMessage email = InitEmail(options,mailBody);
            BodyBuilder builder = await BuildAttachmentsAsync(mailBody);
            SetBody(mailBody, email, builder);
            await SmtpSendAsync(options,email);
        }

        public static void SendEmail(MailOptions options, MailBody mailBody)
        {
            if (mailBody.ToEmails == default || mailBody.ToEmails is null || mailBody.ToEmails.Length == 0)
                throw new ArgumentNullException($"{nameof(mailBody.ToEmails)} cant't  be null or empty");

            MimeMessage email = InitEmail(options,mailBody);
            BodyBuilder builder = BuildAttachments(mailBody);
            SetBody(mailBody, email, builder);
            SmtpSend(options, email);
        }

       

        private static MimeMessage InitEmail(MailOptions options ,MailBody mailBody)
        {
            MimeMessage email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(options.Mail));
            email.To.AddRange(mailBody.ToEmails.Select(e=>MailboxAddress.Parse(e)));
            email.Subject = mailBody.Subject;
            email.Priority = mailBody.Priority;
            return email;
        }

        private static void SetBody(MailBody mailBody, MimeMessage email, BodyBuilder builder)
        {
            builder.HtmlBody = mailBody.Body;
            email.Body = builder.ToMessageBody();
        }

        private static async Task<BodyBuilder> BuildAttachmentsAsync(MailBody mailBody)
        {
            var builder = new BodyBuilder();
            if (mailBody.Attachments != null)
            {
                byte[] fileBytes;
                foreach (IFormFile file in mailBody.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            return builder;
        }

        private static BodyBuilder BuildAttachments(MailBody mailBody)
        {
            var builder = new BodyBuilder();
            if (mailBody.Attachments != null)
            {
                byte[] fileBytes;
                foreach (IFormFile file in mailBody.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            return builder;
        }

        private static async Task SmtpSendAsync(MailOptions options, MimeMessage email)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {                    
                    await smtp.ConnectAsync(options.Host, options.Port, SecureSocketOptions.Auto);
                    if(!string.IsNullOrEmpty(options.Password))
                        await smtp.AuthenticateAsync(options.Mail, options.Password);
                    await smtp.SendAsync(email);
                }
                catch 
                {
                    throw;

                } 
                finally
                {
                    await smtp.DisconnectAsync(true);
                    smtp.Dispose();
                }
            }

        }

        private static void SmtpSend(MailOptions options, MimeMessage email)
        {
            using SmtpClient smtp = new SmtpClient();
            smtp.Connect(options.Host, options.Port, SecureSocketOptions.Auto);
            smtp.Authenticate(options.Mail, options.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
