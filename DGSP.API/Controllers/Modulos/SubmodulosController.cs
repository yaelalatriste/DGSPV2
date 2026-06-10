using DGSP.Module.Modulos.Application.Queries;
using DGSP.Module.Permisos.Domain.DPermisos;
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
    public class SubmodulosController : ControllerBase
    {
        private readonly ISubmodulosQueryService _submodulos;
        private readonly IOpcionesQueryService _opciones;
        private readonly IPermisosQueryService _permisos;

        public SubmodulosController(ISubmodulosQueryService submodulos, IOpcionesQueryService opciones, IPermisosQueryService permisos)
        {
            _submodulos = submodulos;
            _opciones = opciones;
            _permisos = permisos;
        }

        [Route("getSubmodulosByModulo/{modulo}")]
        [HttpGet]
        public async Task<List<SubmoduloDto>> GetSubmodulosByModulo(int modulo)
        {
            var submodulos = await _submodulos.GetSubmoduloByModuloAsync(modulo);

            foreach (var sm in submodulos)
            {
                sm.Opciones = await _opciones.GetOpcionesBySubmoduloAsync(sm.Id);
                foreach (var p in sm.Opciones)
                {
                    p.Permisos = await _permisos.GetPermisosByOpciones(p.Id);
                    foreach (var po in p.Permisos)
                    {
                        po.Permiso = await _permisos.GetPermisoByIdAsync(po.PermisoId);
                    }
                }
            }

            return submodulos;
        }
        
        [Route("getSubmoduloById/{submodulo}")]
        [HttpGet]
        public async Task<SubmoduloDto> GetSubmoduloById(int submodulo)
        {
            return await _submodulos.GetSubmoduloByIdAsync(submodulo);
        }
    }
}
