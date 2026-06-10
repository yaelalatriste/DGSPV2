namespace DGSP.Module.Estatus.Domain.Continuidade
{
    public class FlujoContinuidad
    {
        public int EstatusId { get; set; }
        public int ESucesivoId { get; set; }
        public int PermisoId { get; set; }
        public string Boton { get; set; } = string.Empty;
    }
}
