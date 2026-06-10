namespace DGSP.Modules.SMedicos.Contract.DTOs.Siacom.Dashboards
{
    public class DashboardKpiDto
    {
        public int TotalConsultas { get; set; }
        public string TipoConsultaMasFrecuente { get; set; } = string.Empty;
        public string MesMayorDemanda { get; set; } = string.Empty;
        public decimal VariacionPorcentual { get; set; }
    }
}
