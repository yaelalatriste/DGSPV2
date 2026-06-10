namespace DGSP.Shared.Contracts.DTOs.Estatus.Continuidades
{
    public class FlujoEntregableContinuidadDto
    {
        public int EstatusId { get; set; }
        public int ESucesivoId { get; set; }
        public int EntregableId { get; set; }
        public int PermisoId { get; set; }
        public bool Editable { get; set; }
    }
}
