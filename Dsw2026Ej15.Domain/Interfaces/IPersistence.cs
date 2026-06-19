using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        Speciality GetSpecialtyById(Guid id);
        void AddDoctor(Doctor doctor);
        List<Doctor> GetActiveDoctors();
        Doctor GetDoctorById(Guid id);
        void DeactivateDoctor(Guid id);
    }
}
