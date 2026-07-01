using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;

        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            return await _context.Doctors
                .Include(d => d.Speciality)
                .Where(d => d.IsActive == true)
                .ToListAsync();
        }

        public async Task<Doctor?> GetDoctorById(Guid id)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Speciality)
                .SingleOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                throw new ValidationException("Id", $"El médico con ID {id} no existe.", statusCode: 404);

            if (!doctor.IsActive)
                throw new ValidationException("Id", $"El médico con ID {id} no se encuentra activo.", statusCode: 404);

            return doctor;
        }

        public async Task<Speciality?> GetSpecialityById(Guid id)
        {
            var speciality = await _context.Specialities
                .SingleOrDefaultAsync(d => d.Id == id);
            if (speciality == null) throw new ValidationException("SpecialityId", $"La especialidad con ID {id} no existe.", statusCode: 404);
            return speciality;
        }

        public async Task SaveDoctor(Doctor doctor) {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctor(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
