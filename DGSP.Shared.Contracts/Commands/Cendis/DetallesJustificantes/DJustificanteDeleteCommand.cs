using System;

namespace DGSP.Shared.Contracts.Commands.Cendis.DetallesJustificantes
{
    public class DJustificanteDeleteCommand
    {
        public int Id { get; set; }
        public string Observaciones { get; set; }
        public string Cendi { get; set; }
        public string Mes { get; set; }
        public int Anio { get; set; }
    }
}
