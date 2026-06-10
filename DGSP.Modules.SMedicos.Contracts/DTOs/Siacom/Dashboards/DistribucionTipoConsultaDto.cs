namespace DGSP.Modules.SMedicos.Contract.DTOs.Siacom.Dashboards
{
    public class DistribucionTipoConsultaDto
    {
        public string TipoConsulta { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
