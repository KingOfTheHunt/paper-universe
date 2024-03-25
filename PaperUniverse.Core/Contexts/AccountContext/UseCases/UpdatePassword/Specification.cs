using Flunt.Notifications;
using Flunt.Validations;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdatePassword;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) => 
        new Contract<Notification>()
            .Requires()
            .AreEquals(request.NewPassword, request.NewPasswordAgain, "NewPassword", "As senhas não são iguais.")
            .IsNotNullOrEmpty(request.NewPassword, "Password", "A senha não pode ser nula ou vazia.")
            .IsGreaterOrEqualsThan(request.NewPassword.Length, 6, "NewPassword", "A senha deve ter no mínimo 6 caracteres.")
            .IsLowerOrEqualsThan(request.NewPassword.Length, 20, "NewPassword", "A senha deve ter no máximo 20 caracteres.")
            .IsNotNullOrEmpty(request.NewPasswordAgain, "Password", "A senha não pode ser nula ou vazia.")
            .IsGreaterOrEqualsThan(request.NewPasswordAgain.Length, 6, "Password", "A senha deve ter no mínimo 6 caracteres.")
            .IsLowerOrEqualsThan(request.NewPasswordAgain.Length, 20, "Password", "A senha deve ter no máximo 20 caracteres.");
}