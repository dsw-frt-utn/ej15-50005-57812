using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain;

public class Doctor
{
    public String Name { get; init; }
    public String LicenseNumber { get; init; }
    public bool IsActive { get; private set; }
    public Speciality? Speciality { get; private set; }


}



