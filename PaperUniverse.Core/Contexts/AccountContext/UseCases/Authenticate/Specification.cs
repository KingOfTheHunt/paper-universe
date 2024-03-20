using Flunt.Notifications;
using Flunt.Validations;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Authenticate;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "Email", "E-mail inválido.")
            .IsNotNullOrEmpty(request.Password, "Password", "A senha não pode ser nula ou vazia.")
            .IsGreaterOrEqualsThan(request.Password.Length, 6, "Password", "A senha deve ter no mínimo 6 caracteres.")
            .IsLowerOrEqualsThan(request.Password.Length, 20, "Password", "A senha deve ter no máximo 20 caracteres.");
}