using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.SendResetPasswordCode.Contracts;
using PaperUniverse.Infra.Helpers;

namespace PaperUniverse.Infra.Contexts.AccountContext.UseCases.SendResetPasswordCode;

public class Service : IService
{
    public async Task SendResetPasswordCodeEmailAsync(User user, CancellationToken cancellationToken)
    {
        var smtp = MailHelper.GetSmtp();
        var subject = "Códgo para resetar a senha";
        var body = @$"Olá, {user.Name}! <br /> Aqui está o código para você resetar a sua senha: 
            <strong>{user.Password.ResetCode}</strong>";
        var mail = MailHelper.CreateMailMessage(user.Email.Address, subject, body);

        await smtp.SendMailAsync(mail, cancellationToken);
    }
}