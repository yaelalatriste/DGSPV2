using System;

namespace DGSP.Shared.Contracts.Commands.Cendis.RegistroVisitantes
{
    public class RegistroVisitantesUpdateCommand
    {
        public int Id { get; set; }
        public string Observaciones { get; set; } = string.Empty;
    }
}
