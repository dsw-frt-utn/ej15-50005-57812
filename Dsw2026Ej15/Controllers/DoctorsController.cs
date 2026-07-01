using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase // hereda de controlerbase 
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    #region POST
    [HttpPost] //Inserta un nuevo doctor
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("Name", "El nombre es requerido.");

        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
            throw new ValidationException("LicenseNumber", "La matrícula es requerida.");

        var specialty = await _persistence.GetSpecialityById(request.SpecialityId);

        if(specialty == null)
        {
            throw new ValidationException("SpecialityId", "La especialidad especificada no existe.");
        }

    var newDoctor = new Doctor(request.Name, request.LicenseNumber, specialty, Guid.NewGuid());
        await _persistence.SaveDoctor(newDoctor);

        var response = new DoctorModel.Response(
            newDoctor.Id,
            newDoctor.Name,
            newDoctor.LicenseNumber,
            newDoctor.IsActive,
            newDoctor.Speciality?.Id ?? request.SpecialityId
        );
        return Created($"api/doctors/{response.Id}", response);
    }
    #endregion

    #region GET ACTIVE
    [HttpGet] //Trae a todos los doctores activos
    public async Task<IActionResult> GetActiveDoctors()
    {
        var activeDoctors = await _persistence.GetAllDoctors();

        var response = activeDoctors.Select(doctor => new DoctorModel.Response(
            doctor.Id,
            doctor.Name,
            doctor.LicenseNumber,
            doctor.IsActive,
            doctor.Speciality?.Id ?? Guid.Empty
        ));

        return Ok(response);
    }
    #endregion

    #region GET BY ID
    [HttpGet("{id:guid}")] //Trae a un doctor mediante su Id
    public async Task<IActionResult> GetDoctorById([FromRoute] Guid id)
    {
        var doctor = await _persistence.GetDoctorById(id);
        string specialityName = doctor.Speciality.Name ?? "Sin Especialidad.";

        return Ok(new
        {
            doctor.Name,
            doctor.LicenseNumber,
            specialityName
        });
    }
    #endregion

    #region DELETE
    [HttpDelete("{id:guid}")] //Desactiva a un doctor
    public async Task<IActionResult> DeactivateDoctor([FromRoute] Guid id)
    {
        var doctor = await _persistence.GetDoctorById(id);
        doctor.Deactivate();
        await _persistence.UpdateDoctor(doctor);
        return NoContent();
    }
    #endregion
}
