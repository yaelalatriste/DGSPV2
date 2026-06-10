using DGSP.Module.Modulos.Application.Queries;
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
    public class ModulosController : ControllerBase
    {
        private readonly IModulosQueryService _modulos;
        private readonly ISubmodulosQueryService _submodulos;
        private readonly IOpcionesQueryService _opciones;
        private readonly IPermisosQueryService _permisos;

        public ModulosController(IModulosQueryService modulos, ISubmodulosQueryService submodulos, IPermisosQueryService permisos, IOpcionesQueryService opciones)
        {
            _modulos = modulos;
            _submodulos = submodulos;
            _permisos = permisos;
            _opciones = opciones;
        }

        [HttpGet]

        public async Task<List<ModuloDto>> GetModulosAll()
        {
            var modulos = await _modulos.GetAllModulosAsync();

            foreach (var sb in modulos)
            {
                sb.Submodulos = await _submodulos.GetSubmoduloByModuloAsync(sb.Id);
                if (sb.Submodulos != null)
                {
                    foreach (var sm in sb.Submodulos)
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
                }
            }

            return modulos;
        }

        [Route("{modulo}")]
        [HttpGet]
        public async Task<ModuloDto> GetModuloByIdAsync(int modulo)
        {
            return await _modulos.GetModuloByIdAsync(modulo);
        }

        [HttpGet("getSubmoduloById/{submodulo}")]
        public async Task<SubmoduloDto> GetSubmoduloById(int submodulo)
        {
            var result = await _submodulos.GetSubmoduloByIdAsync(submodulo);

            return result;
        }
    }
}
