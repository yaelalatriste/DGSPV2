using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;
using DGSP.Shared.Contracts.DTOs.Usuarios;

namespace DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;

public class EntregableContinuidadDto
{
    public int Id { get; set; }
    public string UsuarioId { get; set; } = string.Empty;
    public int ContinuidadId { get; set; }
    public int EntregableId { get; set; }
    public string Archivo { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public DateTime? FechaEliminacion { get; set; }

    public CTEntregableDto Entregable { get; set; } = new CTEntregableDto();
    public UsuarioDto Usuario { get; set; } = new UsuarioDto();
}
