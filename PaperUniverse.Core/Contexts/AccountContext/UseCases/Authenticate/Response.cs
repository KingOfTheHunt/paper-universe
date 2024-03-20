using Flunt.Notifications;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Response : SharedContext.UseCases.Response
{
    public ResponseData? Data { get; set; }

    public Response(int status, string message, IEnumerable<Notification>? notifications = null, 
        ResponseData? data = null)
    {
        Status = status;
        Message = message;
        Notifications = notifications;
        Data = data;
    }
}

public class ResponseData 
{
    public string Token { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}