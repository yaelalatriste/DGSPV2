namespace DGSP.Module.Estatus.Domain.NotasTraspaso
{
    public class FlujoNotaTraspaso
    {
        public int EstatusId { get; set; }
        public int ESucesivoId  { get; set; }
        public string Boton { get; set; } = string.Empty;
        public int PermisoId { get; set; }
    }
}
