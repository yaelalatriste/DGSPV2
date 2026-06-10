namespace DGSP.Module.Seguros.Domain.DGSP.Continuidades;

public class Continuidad
{
    public int Id { get; set; }
    public string UsuarioId { get; set; } = null!;
    public int EstatusId { get; set; }
    public int Expediente { get; set; }
    public DateTime? FechaBaja { get; set; }
    public DateTime? FechaEnvioSp { get; set; }
    public DateTime? FechaLimitePago { get; set; }
    public decimal? Importe { get; set; }
    public bool? Pagado { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public DateTime? FechaEliminacion { get; set; }
}
