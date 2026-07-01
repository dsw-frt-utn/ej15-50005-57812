using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dsw2026Ej15.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; }

    protected BaseEntity(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
    }
}
