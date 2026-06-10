namespace DGSP.Modules.SMedicos.Contract.DTOs.Siacom.Reportes
{
    public class RConsultaDto
    {
        public int Anio { get; set; }
        public int MesId { get; set; }
        public string Consulta { get; set; } = null!;
        public int IdConsultorio { get; set; }
        public string Consultorio { get; set; } = null!;
        public string TipoConsultorio { get; set; } = null!;
        public int IdTipoConsulta { get; set; }
        public string TipoConsulta { get; set; } = null!;
        public Nullable<int> IdTipoConsultaDetalle { get; set; }
        public string TipoConsultaDetalle { get; set; } = null!;
        public Nullable<int> IdTipoServicio { get; set; }
        public string TipoServicio { get; set; } = null!;
        public int ExpedientePaciente { get; set; }
        public DateTime FechaConsulta { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
