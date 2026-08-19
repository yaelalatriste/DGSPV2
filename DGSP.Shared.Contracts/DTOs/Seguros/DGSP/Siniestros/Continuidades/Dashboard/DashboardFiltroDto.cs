namespace DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Dashboard
{
    public class DashboardFiltroDto
    {
        public int Anio { get; set; }
        public int Mes { get; set; }
        public string? TipoPersonal { get; set; }
        public int? EstatusId { get; set; }
    }
}
