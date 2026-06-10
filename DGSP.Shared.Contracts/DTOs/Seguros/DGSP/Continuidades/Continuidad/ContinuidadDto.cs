using DGSP.Shared.Contracts.DTOs.DGRH.Empleados;
using DGSP.Shared.Contracts.DTOs.Estatus.Continuidades;
using DGSP.Shared.Contracts.DTOs.Usuarios;

namespace DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;

public class ContinuidadDto
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

    public UsuarioDto Usuario { get; set; } = new UsuarioDto();
    public EstatusContinuidadDto Estatus { get; set; } = new EstatusContinuidadDto();
    public EmpleadoDto ServidorPublico { get; set; } = new EmpleadoDto();
}
