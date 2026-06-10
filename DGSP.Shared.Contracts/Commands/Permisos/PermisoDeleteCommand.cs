using MediatR;

namespace DGSP.Shared.Contracts.Commands.Permisos
{
    public class PermisoDeleteCommand : IRequest<int>
    {
        public string UsuarioId { get; set; } = string.Empty;
        public int ModuloId { get; set; }
    }
}
