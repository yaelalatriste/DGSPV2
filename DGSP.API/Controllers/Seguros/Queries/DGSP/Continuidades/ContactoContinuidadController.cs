using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Seguros.Queries.DGSP.Continuidades
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/seguros/continuidades/[controller]")]
    public class ContactoContinuidadController : ControllerBase
    {
        private readonly IContactoContinuidadService _contactoService;

        public ContactoContinuidadController(IContactoContinuidadService contactoService)
        {
            _contactoService = contactoService;
        }

        [HttpGet]
        [Route("getContactoByContinuidad/{continuidadId}")]
        public async Task<IActionResult> GetContactoByContinuidad(int continuidadId)
        {
            var continuidad = await _contactoService.GetContactosByContinuidad(continuidadId);

            return Ok(continuidad);
        }
    }
}
