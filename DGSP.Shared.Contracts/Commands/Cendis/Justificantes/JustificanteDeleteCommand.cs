using System;

namespace DGSP.Shared.Contracts.Commands.Cendis.Justificantes
{
    public class JustificanteDeleteCommand
    {
        public int Id { get; set; }
        public DateTime FechaEliminacion { get; set; }
    }
}
