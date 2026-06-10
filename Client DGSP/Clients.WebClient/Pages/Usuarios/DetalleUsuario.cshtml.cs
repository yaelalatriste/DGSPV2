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
    public class DetalleModel : PageModel
    {
        private readonly IUsuarioProxy _usuarios;
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        public int Anio { get; set; }
        public ModuloDto Modulo { get; set; }
        public List<ModuloDto> Modulos { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public UsuarioDto Usuario { get; set; }

        public DetalleModel(IUsuarioProxy usuarios, IModuloProxy modulo, IPermisoProxy permisos)
        {
            _usuarios = usuarios;
            _modulo = modulo;
            _permisos = permisos;
        }

        public async Task OnGet(int moduloId, int submoduloId, string usuarioId)
        {
            Permisos = await _permisos.GetPermisosByModuloUsuario(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, moduloId);
            if (Permisos.Where(p => p.Permiso.Nombre.Equals("Ver")).Count() != 0)
            {
                Permisos = await _permisos.GetPermisosByUsuario(usuarioId);
                Modulo = await _modulo.GetModuloByIdAsync(moduloId);
                Submodulo = await _modulo.GetSubmoduloByIdAsync(submoduloId);
                Modulos = await _modulo.GetAllModulosAsync();
                Usuario = await _usuarios.GetUsuarioById(usuarioId);
            }
            else
            {
                Response.Redirect("/error/denegado");
            }
        }
    }
}
