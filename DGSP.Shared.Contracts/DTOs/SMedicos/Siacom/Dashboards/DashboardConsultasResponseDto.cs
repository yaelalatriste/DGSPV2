namespace DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Dashboards
{
    public class DashboardConsultasResponseDto
    {
        public DashboardKpiDto Kpis { get; set; } = new();
        public List<ComparativaTipoConsultaDto> ComparativaTipos { get; set; } = new();
        public List<DistribucionTipoConsultaDto> Distribucion { get; set; } = new();
        public List<TendenciaMensualDto> TendenciaMensual { get; set; } = new();
        public List<DetalleConsultaDto> Detalle { get; set; } = new();
    }
}
