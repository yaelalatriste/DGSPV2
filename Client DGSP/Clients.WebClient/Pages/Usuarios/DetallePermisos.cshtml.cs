using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.Usuarios;
using DGSP.Shared.Contracts.Commands.Permisos;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using DGSP.Shared.Contracts.DTOs.Usuarios;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clients.WebClient.Pages.Usuarios
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class DetallePermisosModel : PageModel
    {
        private readonly IUsuarioProxy _usuarios;
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        public int Anio { get; set; }
        public ModuloDto Modulo { get; set; }
        public ModuloDto ModuloPermisos { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public List<SubmoduloDto> Submodulos { get; set; }
        public OpcionDto Opciones { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public UsuarioDto Usuario { get; set; }

        public DetallePermisosModel(IUsuarioProxy usuarios, IModuloProxy modulo, IPermisoProxy permisos)
        {
            _usuarios = usuarios;
            _modulo = modulo;
            _permisos = permisos;
        }

        public async Task OnGet(int moduloId, int submoduloId, string usuarioId, int modulo)
        {
            Permisos = await _permisos.GetPermisosByModuloUsuario(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, moduloId);
            if (Permisos.Where(p => p.Permiso.Nombre.Equals("Ver")).Count() != 0)
            {
                Permisos = await _permisos.GetPermisosByUsuario(usuarioId);
                Modulo = await _modulo.GetModuloByIdAsync(moduloId);
                ModuloPermisos = await _modulo.GetModuloByIdAsync(modulo);
                Submodulo = await _modulo.GetSubmoduloByIdAsync(submoduloId);
                Submodulos = await _modulo.GetSubmodulosByModulo(modulo);
                Usuario = await _usuarios.GetUsuarioById(usuarioId);
            }
            else
            {
                Response.Redirect("/error/denegado");
            }
        }

        public async Task<IActionResult> OnDeleteBorrarPermisos(string usuario, int modulo)
        {
            await _permisos.DeletePermisos(usuario, modulo);
            return this.StatusCode(200);
        }

        public async Task<IActionResult> OnPostCrearPermisos([FromBody] List<PermisoCreateCommand> permisos)
        {
            await _permisos.CreatePermisos(permisos);
            return this.StatusCode(200);
        }
    }
}
