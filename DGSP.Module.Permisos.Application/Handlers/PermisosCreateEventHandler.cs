using DGSP.Module.Permisos.Domain.DPermisos;
using DGSP.Shared.Contracts.Commands.Permisos;
using MediatR;
using Permisos.Persistence.Database;

namespace Permisos.Service.EventHandler.Handlers
{
    public class PermisosCreateEventHandler: INotificationHandler<PermisoCreateCommand>
    {
        private readonly PermisosDbContext _context;

        public PermisosCreateEventHandler(PermisosDbContext context)
        {
            _context = context;
        }

        public async Task Handle(PermisoCreateCommand permisos, CancellationToken cancellationToken)
        {
            var permiso = new PermisoUsuario
            {
                UsuarioId = permisos.UsuarioId,
                ModuloId = permisos.ModuloId,
                SubmoduloId = permisos.SubmoduloId,
                PermisoId = permisos.PermisoId,
                OpcionId = permisos.OpcionId,
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            };

            await _context.AddAsync(permiso);
            await _context.SaveChangesAsync();
        }
    }
}
