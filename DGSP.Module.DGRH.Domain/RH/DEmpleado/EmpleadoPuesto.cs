namespace DGSP.Module.DGRH.Domain.RH.DEmpleado
{
    public class EmpleadoPuesto
    {
        public int Expediente { get; set; }
        public short CscNomb { get; set; }
        public string Paterno { get; set; } = null!;
        public string Materno { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime? FechaInicioNombr { get; set; }
        public DateTime? FechaFinNombr { get; set; }
        public short CscPuesto { get; set; }
        public string CvePuesto { get; set; }
        public string Puesto { get; set; }
        public string Nivel { get; set; }
        public string Rango { get; set; }
        public string Baja { get; set; }
        public string Curp { get; set; }
        public string Rfc { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}