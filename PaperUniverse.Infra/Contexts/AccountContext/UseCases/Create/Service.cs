using System.Net;
using System.Net.Mail;
using PaperUniverse.Core;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts;

namespace PaperUniverse.Infra.Contexts.AccountContext.UseCases.Create;

public class Service : IService
{
    public async Task SendVerificationEmailAsync(User user)
    {
        var smtpClient = new SmtpClient(Configuration.Smtp.Server, Configuration.Smtp.Port)
        {
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(Configuration.Smtp.Username,
            Configuration.Smtp.Password),
            EnableSsl = true
        };

        var mail = new MailMessage();
        mail.From = new MailAddress(Configuration.Smtp.Username);
        mail.To.Add(user.Email.Address);
        mail.Subject = "Código de ativação";
        mail.Body = $"O seu código de ativação é {user.Email.Verification.Code}";

        await smtpClient.SendMailAsync(mail);
    }
}