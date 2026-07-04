using System;
using System.Collections.Generic;
using System.Linq;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;

namespace Dsw2026Ej15.Data;

public class PersistenceEf : IPersistence
{
    private readonly AplicationDbContext _context;

    // El constructor recibe el contexto de Entity Framework
    public PersistenceEf(AplicationDbContext context)
    {
        _context = context;
    }

    // 1. Buscar especialidad por su Id
    public Speciality GetSpecialtyById(Guid id)
    {
        return _context.Specialities.FirstOrDefault(s => s.Id == id)!;
    }

    // 2. Agregar un nuevo doctor a la base de datos
    public void AddDoctor(Doctor newDoctor)
    {
        _context.Doctors.Add(newDoctor);
        _context.SaveChanges(); // Guarda el insert de forma real
    }

    // 3. Obtener la lista de doctores que están activos
    public List<Doctor> GetActiveDoctors()
    {
        // Asumiendo que tu entidad Doctor tiene la propiedad IsActive
        return _context.Doctors.Where(d => d.IsActive).ToList();
    }

    // 4. Buscar un doctor específico por su Id
    public Doctor GetDoctorById(Guid id)
    {
        return _context.Doctors.FirstOrDefault(d => d.Id == id)!;
    }

    // 5. Desactivar un doctor (Baja lógica)
    public void DeactivateDoctor(Guid id)
    {
        var doctor = _context.Doctors.FirstOrDefault(d => d.Id == id);
        if (doctor != null)
        {
            doctor.IsActive = false; // Cambiamos el estado
            _context.SaveChanges();   // Impacta la actualización en la BD
        }
    }
}