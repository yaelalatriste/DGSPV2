using System.Collections.Generic;
using System.Threading.Tasks;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Shared.Contracts.DTOs.Modulos;

namespace Clients.Services
{
    public class PermisosServicios
    {
        private readonly IPermisoProxy _permisos;

        public PermisosServicios(IPermisoProxy permisos)
        {
            _permisos = permisos;
        }

        public async Task<List<ModuloDto>> GetModulosByUsuario(string usuario)
        {
            var modulos = await _permisos.GetModulosByUsuario(usuario);

            return modulos;
        }
    }
}
