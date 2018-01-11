using System.Net.Mail;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Xm.Trial.Services
{
    public class SendMailService : IMailSender
    {
        private MailMessage mailMsg;
        private SmtpClient smtp;

        public SendMailService(IAppConfiguration appConfig)
        {
            smtp = new SmtpClient
            {
                Host = appConfig.EmailData.Host,
                Port = appConfig.EmailData.Port,
                Credentials = new NetworkCredential(appConfig.EmailData.MailAddress, appConfig.EmailData.Password),
                EnableSsl = true
            };

            mailMsg = new MailMessage(appConfig.EmailData.MailAddress, appConfig.EmailData.MailTo);
        }

        public Task SendEmailAsync(string subject, string messageBody, bool isHtmlBody, List<string> attachmentsPaths)
        {
            mailMsg.Body = messageBody;
            mailMsg.Subject = subject;
            mailMsg.IsBodyHtml = isHtmlBody;

            if (attachmentsPaths != null)
            {
                foreach (string str in attachmentsPaths)
                {
                    mailMsg.Attachments.Add(new Attachment(str));
                }
            }

            smtp.Send(mailMsg);

            return Task.FromResult(0);
        }
    }
}