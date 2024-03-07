using Flunt.Validations;
using PaperUniverse.Core.Contexts.SharedContext.ValueObjects;
using SecureIdentity.Password;

namespace PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

public class Password : ValueObject
{
    public string Hash { get; private set; } = string.Empty;
    public string ResetCode { get; set; } = Guid.NewGuid().ToString("N")[..8].ToUpper();

    public Password()
    {
    }

    public Password(string password)
    {
        AddNotifications(new Contract<Password>()
            .Requires()
            .IsNullOrEmpty(password, "Password", "A senha não pode ser nula ou vazia.")
            .IsGreaterOrEqualsThan(password.Length, 6, "Password", "A senha deve ter no mínimo 6 caracteres.")
            .IsLowerOrEqualsThan(password.Length, 20, "Password", "A senha deve ter no máximo 20 caracteres."));
        
        if (IsValid)
            Hash = PasswordHasher.Hash(password);
    }

    public bool Challenge(string password)
    {
        var passwordHash = PasswordHasher.Hash(password);

        return PasswordHasher.Verify(Hash, passwordHash);
    }
}