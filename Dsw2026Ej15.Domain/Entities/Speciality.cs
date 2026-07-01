namespace Dsw2026Ej15.Domain.Entities;

public class Speciality: BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    protected Speciality() { }

    public Speciality(string name, string description, Guid? id = null) : base(id) {
        Name = name;
        Description = description;
    }
}
