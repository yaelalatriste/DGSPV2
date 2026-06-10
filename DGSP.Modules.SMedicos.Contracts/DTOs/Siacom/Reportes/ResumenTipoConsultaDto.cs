namespace DGSP.Modules.SMedicos.Contract.DTOs.Siacom.Reportes
{
    public class ResumenTipoConsultaDto
    {
        public int IdTipoConsulta { get; set; }
        public string TipoConsulta { get; set; } = null!;
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int Cantidad { get; set; }
    }
}
