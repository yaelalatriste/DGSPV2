namespace DGSP.Module.Seguros.Domain.DGSP.Siniestros.Continuidades;

public class EntregableContinuidad
{
    public int Id { get; set; }
    public string UsuarioId { get; set; } = string.Empty;
    public int ContinuidadId { get; set; }
    public int EntregableId { get; set; } 
    public string Archivo { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public DateTime? FechaEliminacion { get; set; }
}
