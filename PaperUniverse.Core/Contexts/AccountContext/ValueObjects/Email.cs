using Flunt.Validations;
using PaperUniverse.Core.Contexts.SharedContext.ValueObjects;

namespace PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

public class Email : ValueObject
{
    public string Address { get; } = string.Empty;
    public Verification Verification { get; private set; } = null!;

    public Email(string addrees)
    {
        Address = addrees;
        Verification = new Verification();

        AddNotifications(new Contract<Email>()
            .Requires()
            .IsEmail(Address, "Address", "O e-mail informado não é válido!"));
        AddNotifications(Verification);
    }

    public void ResendVerificationCode()
    {
        Verification = new Verification();
    }
}