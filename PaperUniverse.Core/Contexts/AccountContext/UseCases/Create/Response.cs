using Flunt.Notifications;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Create;

public class Response : SharedContext.UseCases.Response
{
    public Guid Id { get; }

    public Response(int status, string message, IEnumerable<Notification>? notifications = null)
    {
        Status = status;
        Message = message;
        Notifications = notifications;
    }

    public Response(Guid id, int status, string message)
    {
        Id = id;
        Status = status;
        Message = message;
    }
}