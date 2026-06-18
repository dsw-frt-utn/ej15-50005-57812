using Dsw2026Ej15.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase // hereda de controlerbase 
{
    public DoctorsController(IPersistence persistence)
    {
        persistence = _persistence;
    }


    [HttpPost]
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorModel.Request request)
    {
        if(string.IsNullOrWhiteSpace(request.Name) ||
           string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            return BadRequest("Nombre y Matricula requeridos");
        }

        var specialty= _persistencia.GetSpecialtyById(request.SpecialityId);
        return Ok(); //OK representa el code de estado 200
    }
     
}
