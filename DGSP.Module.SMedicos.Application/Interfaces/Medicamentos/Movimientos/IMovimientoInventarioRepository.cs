using DGSP.Module.SMedicos.Domain.Movimientos;

namespace DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Movimientos
{
    public interface IMovimientoInventarioRepository
    {
        Task<List<MovimientoInventario>> GetMovimientosInventariosByLoteAsync(int lote);
        Task<List<MovimientoInventario>> GetAllMovimientosInventariosAsync();
        Task AddAsync(MovimientoInventario entity);
        Task SaveChangesAsync();

    }
}
