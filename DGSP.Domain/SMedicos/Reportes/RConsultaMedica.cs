namespace DGSP.Domain.SMedicos.Reportes
{
    public class RConsultaMedica
    {
        public int IdConsultorio { get; set; }
        public string Consultorio { get; set; } = null!;
        public int IdTipoConsulta { get; set; }
        public string TipoConsulta { get; set; } = null!;
        public int ExpedientePaciente { get; set; }
        public DateTime FechaConsulta { get; set; }
    }
}
