using PaperUniverse.Core.Contexts.SharedContext.ValueObjects;

namespace PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

public class Verification : ValueObject
{
    public string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsActive => ExpiresAt == null && VerifiedAt != null;

    public Verification()
    {
    }

    public void Verify(string code)
    {
        if (ExpiresAt != null && DateTime.UtcNow > ExpiresAt)
            AddNotification("Verification.Code", "O código de ativação expirou.");
        else if (VerifiedAt != null)
            AddNotification("Verification.Code", "A conta já está ativada.");
        else if (string.Equals(Code.Trim(), code.Trim(), StringComparison.CurrentCultureIgnoreCase) == false)
            AddNotification("Verification.Code", "O código de ativação inválido.");
        else 
        {
            ExpiresAt = null;
            VerifiedAt = DateTime.UtcNow;
        }
    }
}