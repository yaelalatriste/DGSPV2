using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DGSP.Shared.Contracts.Commands.Seguros.Continuidades.CEntregables
{
    public class RegistrarEntregableContinuidadCommand : IRequest<EntregableContinuidadDto>
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int ContinuidadId { get; set; }
        public int EntregableId { get; set; }
        public IFormFile Entregable { get; set; }

        //Almaceniamiento de Entregable
        public int Expediente { get; set; }
        public string TipoEntregable { get; set; } = string.Empty;
        
        //Actualizar los datos en la Continuidad
        public DateTime FechaEnvioSP { get; set; }
        public DateTime FechaLimitePago { get; set; }
        public decimal Importe { get; set; }

        //Log Entregables
        public string Observaciones { get; set; } = string.Empty;
    }
}
