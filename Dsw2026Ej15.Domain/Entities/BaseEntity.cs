namespace Dsw2026Ej15.Domain.Entities;

public abstract class BaseEntity
{
    protected BaseEntity(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
    }
    public Guid Id { get; }
}
