using DGSP.Module.Permisos.Domain.DPermisos;
using DGSP.Shared.Contracts.Commands.Permisos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Permisos.Persistence.Database;

namespace Permisos.Service.EventHandler.Permisos
{
    public class PermisosDeleteEventHandler : IRequestHandler<PermisoDeleteCommand, int>
    {
        private readonly PermisosDbContext _context;

        public PermisosDeleteEventHandler(PermisosDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(PermisoDeleteCommand permisos, CancellationToken cancellationToken)
        {
            List<PermisoUsuario> permiso = await _context.PermisosUsuario.Where(p => p.UsuarioId.Equals(permisos.UsuarioId) && p.ModuloId == permisos.ModuloId).ToListAsync();
            _context.PermisosUsuario.RemoveRange(permiso);

            await _context.SaveChangesAsync();
            return permiso.Count;
        }
    }
}
