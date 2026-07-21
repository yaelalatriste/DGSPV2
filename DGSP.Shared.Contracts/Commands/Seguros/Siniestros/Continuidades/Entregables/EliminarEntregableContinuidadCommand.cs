using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.Entregables
{
    public class EliminarEntregableContinuidadCommand : IRequest<EntregableContinuidadDto>
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int ContinuidadId { get; set; }
        public string Archivo{ get; set; } = string.Empty;

        //Para guardar el archivo
        public int Expediente { get; set; }
        public string TipoEntregable { get; set; } = string.Empty;

        //Log Entregables
        public string Observaciones { get; set; } = string.Empty;
    }
}
