using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Movimientos;
using DGSP.Module.SMedicos.Application.Services.Medicamentos.Movimientos;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Movimientos;

namespace DGSP.Module.SMedicos.Persistence.Services.Medicamentos.Movimientos
{
    public class MovimientoInventarioService : IMovimientoInventarioService
    {
        private readonly IMovimientoInventarioRepository _movimientoInventarioRepository;

        public MovimientoInventarioService(IMovimientoInventarioRepository movimientoInventarioRepository)
        {
            _movimientoInventarioRepository = movimientoInventarioRepository;
        }

        public async Task<List<MovimientoInventarioDto>> GetAllMovimientosInventariosAsync()
        {
            var movimientos = await _movimientoInventarioRepository.GetAllMovimientosInventariosAsync();

            return movimientos.Select(m => new MovimientoInventarioDto {
                Id = m.Id,
                UsuarioId = m.UsuarioId,
                LoteId = m.LoteId,
                TipoMovimiento = m.TipoMovimiento,
                Origen = m.Origen,
                ReferenciaId = m.ReferenciaId,
                SalidaDetalleId = m.SalidaDetalleId,
                Cantidad = m.Cantidad,
                CantidadEnvase = m.CantidadEnvase,
                CantidadTotal = m.CantidadTotal,
                ExistenciaAnterior = m.ExistenciaAnterior,
                ExistenciaPosterior = m.ExistenciaPosterior,
                FechaMovimiento = m.FechaMovimiento,
                Observaciones = m.Observaciones
            }).ToList();
        }

        public async Task<List<MovimientoInventarioDto>> GetMovimientosInventariosByLoteAsync(int loteId)
        {
            var movimientos = await _movimientoInventarioRepository.GetMovimientosInventariosByLoteAsync(loteId);

            return movimientos.Select(m => new MovimientoInventarioDto {
                Id = m.Id,
                UsuarioId = m.UsuarioId,
                LoteId = m.LoteId,
                TipoMovimiento = m.TipoMovimiento,
                Origen = m.Origen,
                ReferenciaId = m.ReferenciaId,
                SalidaDetalleId = m.SalidaDetalleId,
                Cantidad = m.Cantidad,
                CantidadEnvase = m.CantidadEnvase,
                CantidadTotal = m.CantidadTotal,
                ExistenciaAnterior = m.ExistenciaAnterior,
                ExistenciaPosterior = m.ExistenciaPosterior,
                FechaMovimiento = m.FechaMovimiento,
                Observaciones = m.Observaciones
            }).ToList();
        }
    }
}
