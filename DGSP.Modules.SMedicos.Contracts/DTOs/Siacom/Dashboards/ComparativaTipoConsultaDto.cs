namespace DGSP.Modules.SMedicos.Contract.DTOs.Siacom.Dashboards
{
    public class ComparativaTipoConsultaDto
    {
        public string TipoConsulta { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
    }
}
