using Flunt.Notifications;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Details;

public class Response : SharedContext.UseCases.Response
{
    public ResponseData? Data { get; set; }

    public Response(int status, string message, ResponseData? data = null, 
        IEnumerable<Notification>? notifications = null)
    {
        Status = status;
        Message = message;
        Data = data;
        Notifications = notifications;
    }
}

public record ResponseData(string Name, string Email, string Image);