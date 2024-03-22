using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using PaperUniverse.Infra.Helpers;

namespace PaperUniverse.Infra.Contexts.AccountContext.UseCases.Create;

public class Service : IService
{
    public async Task SendVerificationEmailAsync(User user)
    {
        var smtpClient = MailHelper.GetSmtp();

        var subject = "Código de ativação";
        var body = @$"Seja bem vindo, {user.Name}!
            <br /> O seu código de ativação é <strong>{user.Email.Verification.Code}</strong>
            <br /> O seu código de ativação expira em cinco minutos.";

        var mail = MailHelper.CreateMailMessage(user.Email.Address, subject, body);
        
        await smtpClient.SendMailAsync(mail);
    }
}