using DGSP.Shared.Contracts.DTOs.Permisos;
using Microsoft.EntityFrameworkCore;
using Permisos.Persistence.Database;
using Permisos.Service.Queries.Mapping;

namespace Permisos.Service.Queries
{
    public interface IPermisosQueryService
    {
        Task<List<PermisoDto>> GetAllPermisosAsync();
        Task<PermisoDto> GetPermisoByIdAsync(int permiso);
        Task<IEnumerable<PermisoUsuarioDto>> GetPermisosByUsuarioAsync(string usuario);
        Task<List<PermisoUsuarioDto>> GetPermisosByModuloUsuario(string usuario, int modulo);
        Task<List<PermisoOpcionDto>> GetPermisosByOpciones(int opcion);
    }

    public class PermisosQueryService : IPermisosQueryService
    {
        private readonly PermisosDbContext _context;
        public PermisosQueryService(PermisosDbContext context)
        {
            _context = context;
        }

        public async Task<List<PermisoDto>> GetAllPermisosAsync()
        {
            var collection = await _context.Permisos.OrderBy(x => x.Id).ToListAsync();

            return collection.MapTo<List<PermisoDto>>();
        }

        public async Task<PermisoDto> GetPermisoByIdAsync(int permiso)
        {
            return (await _context.Permisos.SingleAsync(x => x.Id == permiso)).MapTo<PermisoDto>();
        }

        public async Task<IEnumerable<PermisoUsuarioDto>> GetPermisosByUsuarioAsync(string usuario)
        {
            try
            {
                var collection = await _context.PermisosUsuario.Where(u => u.UsuarioId.Equals(usuario)).OrderBy(x => x.UsuarioId == usuario).ToListAsync();

                return collection.MapTo<IEnumerable<PermisoUsuarioDto>>();
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }
        
        public async Task<List<PermisoUsuarioDto>> GetPermisosByModuloUsuario(string usuario, int modulo)
        {
            var collection = await _context.PermisosUsuario.Where(p => p.UsuarioId.Equals(usuario) && p.ModuloId == modulo)
                            .OrderBy(x => x.UsuarioId == usuario).ToListAsync();

            return collection.MapTo<List<PermisoUsuarioDto>>();
        }

        public async Task<List<PermisoOpcionDto>> GetPermisosByOpciones(int opcion)
        {
            var collection = await _context.PermisosOpciones.Where(p => p.OpcionId == opcion).OrderBy(x => x.PermisoId).ToListAsync();

            return collection.MapTo<List<PermisoOpcionDto>>();
        }
    }
}
