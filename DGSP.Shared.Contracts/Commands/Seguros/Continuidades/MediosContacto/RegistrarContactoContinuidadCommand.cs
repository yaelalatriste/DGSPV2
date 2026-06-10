using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.Seguros.Continuidades.MediosContacto
{
    public class RegistrarContactoContinuidadCommand : IRequest<ContactoContinuidadDto>
    {
        public int Id { get; set; }
        public int TipoId { get; set; }
        public int ContinuidadId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}
