using MediatR;

namespace DGSP.Shared.Contracts.Commands.Permisos
{
    public class PermisoCreateCommand : INotification
    {
        public string UsuarioId { get; set; } = string.Empty;
        public int ModuloId { get; set; }
        public int SubmoduloId { get; set; }
        public int PermisoId { get; set; }
        public int OpcionId { get; set; }
    }
}
