using System;

namespace DGSP.Shared.Contracts.Commands.Cendis.RegistroVisitantes
{
    public class RegistroVisitantesCreateCommand
    {
        public int Id { get; set; }
        public string TipoVisitante { get; set; } = string.Empty;
        public int Expediente { get; set; }
        public int CendiId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Paterno { get; set; } = string.Empty;
        public string Materno { get; set; } = string.Empty;
        public string MotivoVisita { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
    }
}
