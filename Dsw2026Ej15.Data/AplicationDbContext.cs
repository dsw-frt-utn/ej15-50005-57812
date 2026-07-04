using Dsw2026Ej15.Domain;
using Microsoft.EntityFrameworkCore;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Data;

public class AplicationDbContext : DbContext
{
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
    {
    }

    
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Speciality> Specialities { get; set; }
}
