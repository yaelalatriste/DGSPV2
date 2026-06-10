namespace DGSP.Modules.SMedicos.Contract.DTOs.Siacom.Dashboards
{
    public class DetalleConsultaDto
    {
        public string TipoConsulta { get; set; } = string.Empty;
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int Cantidad { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
