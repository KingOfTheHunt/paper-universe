using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;
using PaperUniverse.Core.Contexts.SharedContext.Entities;

namespace PaperUniverse.Core.Contexts.AccountContext.Entities;

public class User : Entity
{
    public string Name { get; set; } = string.Empty;
    public Email Email { get; set; } = null!;
    public Password Password { get; set; } = null!;
    public string Image { get; set; } = string.Empty;

    protected User() 
    {
    }

    public User(string name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;

        AddNotifications(email, password);
    }

    public void UpdateEmail(Email email) => 
        Email = email;

    public void UpdatePassword(string plainTextPassword)
    {
        var password = new Password(plainTextPassword);
        Password = password;
    }

    public void ResetPassword(string plainTextPassword, string code)
    {
        if (string.Equals(Password.ResetCode, code.Trim(), StringComparison.CurrentCultureIgnoreCase) == false)
            AddNotification("Password.ResetCode", "O código informado é inválido.");

        var password = new Password(plainTextPassword);
        Password = password;
    }
}