namespace DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Dashboard
{
    public class DashboardContinuidadesDto
    {
        public int Anio { get; set; }

        public int Mes { get; set; }

        public string Periodo { get; set; } = string.Empty;

        public DashboardTotalesDto Totales { get; set; } = new();

        public List<DashboardSemanaDto> Semanas { get; set; } = [];

        public List<DashboardEstatusDto> Estatus { get; set; } = [];
    }
}
