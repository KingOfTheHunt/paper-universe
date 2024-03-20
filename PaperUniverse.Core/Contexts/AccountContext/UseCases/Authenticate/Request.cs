using MediatR;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Request : IRequest<Response>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}