using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Movimientos;

namespace DGSP.Module.SMedicos.Application.Services.Medicamentos.Movimientos
{
    public interface IMovimientoInventarioService
    {
        Task<List<MovimientoInventarioDto>> GetAllMovimientosInventariosAsync();
        Task<List<MovimientoInventarioDto>> GetMovimientosInventariosByLoteAsync(int lote);
    }
}
