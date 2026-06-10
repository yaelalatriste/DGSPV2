using System;

namespace DGSP.Shared.Contracts.Commands.Cendis.Justificantes
{
    public class JustificanteCreateCommand
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public int CendiId { get; set; }
        public int Anio { get; set; }
        public int MesId { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
