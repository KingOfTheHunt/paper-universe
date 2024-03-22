using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.ResendVerification.Contracts;
using PaperUniverse.Infra.Helpers;

namespace PaperUniverse.Infra.Contexts.AccountContext.UseCases.ResendVerification
{
    public class Service : IService
    {
        public async Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
        {
            var smtp = MailHelper.GetSmtp();
            var subject = "Reenvio do código de ativação";
            var body = @$"Olá, {user.Name}! <br />
            Aqui está seu novo código de ativação: <strong>{user.Email.Verification.Code}</strong>.";
            var mail = MailHelper.CreateMailMessage(user.Email.Address, subject, body);

            await smtp.SendMailAsync(mail);
        }
    }
}