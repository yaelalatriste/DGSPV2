namespace DGSP.Shared.Contracts.DTOs.Estatus.Continuidades
{
    public class FlujoContinuidadDto
    {
        public int EstatusId { get; set; }
        public int ESucesivoId { get; set; }
        public int PermisoId { get; set; }
        public string Boton { get; set; } = string.Empty;
    }
}
