using Flunt.Notifications;

namespace PaperUniverse.Core.Contexts.SharedContext.Entities;

public abstract class Entity : Notifiable<Notification>, IEquatable<Guid>
{
    public Guid Id { get; }

    protected Entity() =>
        Id = Guid.NewGuid();

    public bool Equals(Guid id) => 
        Id == id;
}