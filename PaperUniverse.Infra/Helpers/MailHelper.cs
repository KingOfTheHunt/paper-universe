using System.Net;
using System.Net.Mail;
using PaperUniverse.Core;

namespace PaperUniverse.Infra.Helpers
{
    public static class MailHelper
    {
        public static SmtpClient GetSmtp() => 
            new()
            {
                Host = Configuration.Smtp.Host,
                Port = Configuration.Smtp.Port,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Configuration.Smtp.Username,
                    Configuration.Smtp.Password),
                EnableSsl = true
            };

        public static MailMessage CreateMailMessage(string to, string subject, string body)
        {
            var message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(Configuration.Smtp.Username);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;

            return message;
        }
    }
}