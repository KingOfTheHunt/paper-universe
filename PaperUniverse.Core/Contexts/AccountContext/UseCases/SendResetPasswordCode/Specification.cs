using Flunt.Notifications;
using Flunt.Validations;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.SendResetPasswordCode
{
    public static class Specification
    {
        public static Contract<Notification> Assert(Request request) =>
            new Contract<Notification>()
                .Requires()
                .IsEmail(request.Email, "Email", "O e-mail é inválido.");
    }
}