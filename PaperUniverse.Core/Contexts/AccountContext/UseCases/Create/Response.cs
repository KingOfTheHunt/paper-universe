using Flunt.Notifications;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Create;

public class Response : SharedContext.UseCases.Response
{
    public ResponseData? Data { get; set; }

    public Response(int status, string message, IEnumerable<Notification>? notifications = null)
    {
        Status = status;
        Message = message;
        Notifications = notifications;
    }

    public Response(int status, string message, ResponseData data)
    {
        Status = status;
        Message = message;
        Data = data;
    }
}

public record ResponseData(Guid Id);