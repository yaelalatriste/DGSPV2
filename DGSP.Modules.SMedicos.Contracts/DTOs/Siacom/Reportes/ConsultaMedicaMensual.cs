namespace DGSP.Modules.SMedicos.Contract.DTOs.Siacom.Reportes
{
    public class ConsultaMedicaMensual
    {
        public int fIdTipoConsulta { get; set; }
        public string fcTipoConsulta { get; set; } = string.Empty;
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int Cantidad { get; set; }
    }
}
