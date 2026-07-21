using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.MediosContacto
{
    public class RegistrarContactoContinuidadCommand : IRequest<ContactoContinuidadDto>
    {
        public int Id { get; set; }
        public int TipoId { get; set; }
        public int ContinuidadId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}
