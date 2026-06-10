namespace DGSP.Module.Seguros.Domain.DGSP.Continuidades;

public class EntregableContinuidad
{
    public int Id { get; set; }
    public string UsuarioId { get; set; } = string.Empty;
    public int ContinuidadId { get; set; }
    public int EntregableId { get; set; } 
    public string Archivo { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public Nullable<DateTime> FechaActualizacion { get; set; }
    public Nullable<DateTime> FechaEliminacion { get; set; }
}
