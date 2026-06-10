using DGSP.Module.Modulos.Application.Queries;
using DGSP.Shared.Contracts.Commands.Permisos;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Permisos.Service.Queries;

namespace DGSP.API.Controllers.Permisos
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PermisosController : ControllerBase
    {
        private readonly IPermisosQueryService _permisos;
        private readonly IModulosQueryService _modulos;
        private readonly ISubmodulosQueryService _submodulos;
        private readonly IOpcionesQueryService _opciones;
        private readonly IMediator _mediator;

        public PermisosController(IPermisosQueryService permisos, IModulosQueryService modulos, ISubmodulosQueryService submodulos, IMediator mediator, IOpcionesQueryService opciones)
        {
            _permisos = permisos;
            _modulos = modulos;
            _submodulos = submodulos;
            _opciones = opciones;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<PermisoDto>> GetAllPermisos()
        {
            var result = await _permisos.GetAllPermisosAsync();

            return result;
        }

        [HttpGet]
        [Route("getPermisosByUsuario/{usuario}")]
        public async Task<IEnumerable<PermisoUsuarioDto>> GetPermisosByUsuario(string usuario)
        {
            var result = await _permisos.GetPermisosByUsuarioAsync(usuario);

            return result;
        }

        [HttpGet]
        [Route("getModulosByUsuario/{usuario}")]
        public async Task<List<ModuloDto>> GetModulosByUsuario(string usuario)
        {
            var permisos = await _permisos.GetPermisosByUsuarioAsync(usuario);

            var modulos = (await _modulos.GetAllModulosAsync()).Where(m => permisos.Any(p => p.ModuloId == m.Id)).ToList();

            foreach (var modulo in modulos)
            {
                var submodulos = await _submodulos.GetSubmoduloByModuloAsync(modulo.Id);

                modulo.Submodulos = submodulos.Where(sm => permisos.Any(p => p.ModuloId == modulo.Id && p.SubmoduloId == sm.Id)).ToList();

                foreach (var submodulo in modulo.Submodulos)
                {
                    var opciones = await _opciones.GetOpcionesBySubmoduloAsync(submodulo.Id);

                    submodulo.Opciones = opciones.Where(o => permisos.Any(p =>p.ModuloId == modulo.Id &&p.SubmoduloId == submodulo.Id &&p.OpcionId == o.Id)).ToList();
                }
            }

            return modulos;
        }

        [HttpGet]
        [Route("getPermisosByModuloUsuario/{usuario}/{modulo}")]
        public async Task<IEnumerable<PermisoUsuarioDto>> GetPermisosByModuloUsuario(string usuario, int modulo)
        {
            var collection = await _permisos.GetPermisosByModuloUsuario(usuario, modulo);

            foreach (var pr in collection)
            {
                pr.Modulo = await _modulos.GetModuloByIdAsync(pr.ModuloId);
                pr.Submodulo = await _submodulos.GetSubmoduloByIdAsync(pr.SubmoduloId);
                pr.Permiso = await _permisos.GetPermisoByIdAsync(pr.PermisoId);
                pr.Opciones = await _opciones.GetOpcionByIdAsync(pr.OpcionId);
            }

            return collection;
        }

        [Route("createPermisosByUsuario")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<PermisoCreateCommand> permisos)
        {
            foreach (var p in permisos)
            {
                await _mediator.Publish(p);
            }
            return Ok();
        }

        [Route("deletePermisosByUsuario/{usuario}/{modulo}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string usuario, int modulo)
        {
            PermisoDeleteCommand permisos = new PermisoDeleteCommand();
            permisos.UsuarioId = usuario;
            permisos.ModuloId = modulo;
            await _mediator.Send(permisos);
            return Ok();
        }
    }
}
