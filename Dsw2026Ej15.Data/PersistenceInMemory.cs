using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory
{
    private List<Doctor> _doctors = [];
    private List<Speciality> _specialities = [];

    public PersistenceInMemory()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        LoadSpecialties();
    }

    private void LoadSpecialties()
    {
        string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                          "Sources", "specialities.json");
        var json = File.ReadAllText(jsonPath);
        var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        }) ?? [];

        _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
    }

    public Speciality GetSpecialtyById(Guid id)
    {
        var speciality = _specialities.FirstOrDefault(s => s.Id == id);
        if (speciality == null) 
            throw new ValidationException("SpecialityId", $"La especialidad con ID {id} no existe.", statusCode: 404);
        return speciality;
    }

    public void AddDoctor(Doctor doctor)
    {
        _doctors.Add(doctor);
    }

    public List<Doctor> GetActiveDoctors()
    {
        return _doctors.Where(d => d.IsActive).ToList();
    }

    public Doctor GetDoctorById(Guid id)
    {
        var doctor = _doctors.FirstOrDefault(d => d.Id == id);
        
        if (doctor == null)
           throw new ValidationException("Id",$"El médico con ID {id} no existe.", statusCode: 404);

        if (!doctor.IsActive)
           throw new ValidationException("Id",$"El médico con ID {id} no se encuentra activo.", statusCode: 404);

        return doctor;
    }

    public void DeactivateDoctor(Guid id)
    {
        var doctor = GetDoctorById(id);
        doctor.Deactivate();
    }
}