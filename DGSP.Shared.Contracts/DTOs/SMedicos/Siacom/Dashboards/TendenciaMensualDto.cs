namespace DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Dashboards
{
    public class TendenciaMensualDto
    {
        public int Anio { get; set; }
        public int Mes { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public int TotalConsultas { get; set; }
    }
}
