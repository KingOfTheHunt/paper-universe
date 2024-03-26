using MediatR;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.SendResetPasswordCode;

public class Request : IRequest<Response>
{
    public string Email { get; set; } = string.Empty;
}