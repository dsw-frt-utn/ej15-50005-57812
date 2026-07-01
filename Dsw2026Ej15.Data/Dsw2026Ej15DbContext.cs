using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data;

public class Dsw2026Ej15DbContext : DbContext
{
    public Dsw2026Ej15DbContext(DbContextOptions<Dsw2026Ej15DbContext> options) : base(options)
    { }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Speciality> Specialities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Doctor>(entity => {
            entity.ToTable("Doctors");
            entity.HasKey(d => d.Id);

            entity.Property(d => d.LicenseNumber)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(d => d.IsActive);

            entity.Property(d => d.SpecialityId);

            entity.HasOne(d => d.Speciality)
                  .WithMany()
                  .HasForeignKey(d => d.SpecialityId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Speciality>(entity => {
            entity.ToTable("Specialities");
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(150);

            entity.Property(s => s.Description)
            .HasMaxLength(400);
        });
    }
}