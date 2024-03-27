using MediatR;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.ResetPassword;

public class Request : IRequest<Response>
{
    public string Email { get; set; } = string.Empty;
    public string ResetCode { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string NewPasswordAgain { get; set; } = string.Empty;
}