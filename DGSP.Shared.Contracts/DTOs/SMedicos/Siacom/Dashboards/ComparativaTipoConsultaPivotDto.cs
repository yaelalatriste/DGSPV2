namespace DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Dashboards
{
    public class ComparativaTipoConsultaPivotDto
    {
        public string TipoConsulta { get; set; } = string.Empty;
        public int CantidadMesActual { get; set; }
        public int CantidadMesAnterior { get; set; }
        public int Total { get; set; }
    }
}
