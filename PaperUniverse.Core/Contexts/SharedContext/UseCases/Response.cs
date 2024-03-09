using Flunt.Notifications;

namespace PaperUniverse.Core.Contexts.SharedContext.UseCases;

public abstract class Response
{
    public int Status { get; set; } = 200;
    public string Message { get; set; } = string.Empty;
    public IEnumerable<Notification>? Notifications { get; set; }
    public bool Success => Status >= 200 && Status <= 299;
}