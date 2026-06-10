namespace DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Dashboards
{
    public class ComparativaTipoConsultaDto
    {
        public string TipoConsulta { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
    }
}
