namespace DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Dashboards
{
    public class DistribucionTipoConsultaDto
    {
        public string TipoConsulta { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
