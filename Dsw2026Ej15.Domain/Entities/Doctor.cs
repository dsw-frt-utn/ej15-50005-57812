using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities;

[Table("Doctors")]
public class Doctor : BaseEntity
{
    [Required]
    public string LicenseNumber { get; init; }

    [Required]
    public string Name { get; init; }

    public bool IsActive { get; private set; }

    public Guid SpecialityId { get; private set; }

    public Speciality Speciality { get; private set; } = null!;

    protected Doctor()
    {

    }

    public Doctor(string name, string licenseNumber, Speciality speciality, Guid? id = null) : base(id)
    {
        Name = name;
        LicenseNumber = licenseNumber;
        Speciality = speciality;
        SpecialityId = speciality.Id;
        IsActive = true;
    }

    public void Deactivate()
    {
        this.IsActive = false;
    }
}