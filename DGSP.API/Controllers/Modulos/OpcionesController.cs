using DGSP.Module.Modulos.Application.Queries;
using DGSP.Module.Modulos.Domain.DModulos;
using DGSP.Shared.Contracts.DTOs.Modulos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Permisos.Service.Queries;

namespace DGSP.API.Controllers.Modulos
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class OpcionesController : ControllerBase
    {
        private readonly ISubmodulosQueryService _submodulos;
        private readonly IOpcionesQueryService _opciones;
        private readonly IPermisosQueryService _permisos;

        public OpcionesController(ISubmodulosQueryService submodulos, IOpcionesQueryService opciones, IPermisosQueryService permisos)
        {
            _submodulos = submodulos;
            _opciones = opciones;
            _permisos = permisos;
        }

        [Route("getOpcionById/{id}")]
        [HttpGet]
        public async Task<OpcionDto> GetAllOpcionesAsync(int id)
        {
            return await _opciones.GetOpcionByIdAsync(id);
        }
    }
}
