using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;

namespace DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad
{
    public class ContactoContinuidadDto
    {
        public int Id { get; set; }
        public int TipoId { get; set; }
        public int ContinuidadId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? FechaEliminacion{ get; set; }

        public CTVariableMedicaDto TipoContacto { get; set; } = new CTVariableMedicaDto();
    }
}
