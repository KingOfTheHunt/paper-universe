using Flunt.Notifications;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Verify;

public class Response : SharedContext.UseCases.Response
{
    public Response(int status, string message, IEnumerable<Notification>? notifications = null)
    {
        Status = status;
        Message = message;
        Notifications = notifications;
    }
}