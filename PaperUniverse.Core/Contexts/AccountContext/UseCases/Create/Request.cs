using MediatR;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Create;

public class Request : IRequest<Response>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}