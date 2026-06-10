using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Entradas;
using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Movimientos;
using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Salidas;
using DGSP.Module.SMedicos.Application.Services.Medicamentos.Salidas;
using DGSP.Module.SMedicos.Domain.Inventarios;
using DGSP.Module.SMedicos.Domain.Movimientos;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Salidas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Salidas;

namespace DGSP.Module.SMedicos.Persistence.Services.Medicamentos.Salidas
{
    public class SalidaMedicamentoDetalleService : ISalidaMedicamentoDetalleService
    {
        private readonly ISalidaMedicamentoDetalleRepository _salidaMedicamentoDetalleRepository;
        private readonly IMovimientoInventarioRepository _movimientoRepository;
        private readonly ILoteMedicamentoRepository _loteRepository;

        public SalidaMedicamentoDetalleService(ISalidaMedicamentoDetalleRepository salidaMedicamentoDetalleRepository, IMovimientoInventarioRepository movimientoRepository, 
            ILoteMedicamentoRepository loteRepository)
        {
            _salidaMedicamentoDetalleRepository = salidaMedicamentoDetalleRepository;
            _movimientoRepository = movimientoRepository;
            _loteRepository = loteRepository;
        }

        public async Task<SalidaMedicamentoDetalleDto> GetDetalleByIdAsync(int id)
        {
            var salidaDetalle = await _salidaMedicamentoDetalleRepository.GetByIdAsync(id);

            return new SalidaMedicamentoDetalleDto
            {
                Id = salidaDetalle.Id,
                UsuarioId = salidaDetalle.UsuarioId,
                SalidaId = salidaDetalle.SalidaId,
                ConsultorioDestinoId = salidaDetalle.ConsultorioDestinoId,
                LoteId = salidaDetalle.LoteId,
                TipoInsumoId = salidaDetalle.TipoInsumoId,
                TipoMovimientoId = salidaDetalle.TipoMovimientoId,
                FormaFarmaceuticaId = salidaDetalle.FormaFarmaceuticaId,
                TipoEnvaseId = salidaDetalle.TipoEnvaseId,
                Cantidad = salidaDetalle.Cantidad,
                CantidadEnvase = salidaDetalle.CantidadEnvase,
                Observaciones = salidaDetalle.Observaciones,
                FechaCreacion = DateTime.Now
            };
        }

        public async Task<List<SalidaMedicamentoDetalleDto>> GetDetallesBySalidaAsync(int salida)
        {
            var detallesSalida = await _salidaMedicamentoDetalleRepository.GetDetallesBySalidaIdAsync(salida);

            return detallesSalida.Select(dt => new SalidaMedicamentoDetalleDto
            {
                Id = dt.Id,
                SalidaId = dt.SalidaId,
                ConsultorioDestinoId = dt.ConsultorioDestinoId,
                LoteId = dt.LoteId,
                TipoInsumoId = dt.TipoInsumoId,
                TipoMovimientoId = dt.TipoMovimientoId,
                Cantidad = dt.Cantidad,
                CantidadEnvase = dt.CantidadEnvase,
                FormaFarmaceuticaId = dt.FormaFarmaceuticaId,
                TipoEnvaseId = dt.TipoEnvaseId,
                Observaciones = dt.Observaciones,
                FechaCreacion = DateTime.Now
            }).ToList();
        }

        public async Task<SalidaMedicamentoDetalleDto> RegistrarDetalleSalidaMedicamentoAsync(RegistrarSalidaMedicamentoDetalleCommand command)
        {
            var lote = await _loteRepository.GetByIdAsync(command.LoteId);

            var existenciaAnterior = lote.CantidadTotal;

            lote.Cantidad -= command.Cantidad;
            lote.CantidadTotal -= (command.Cantidad * command.CantidadEnvase);
            lote.FechaActualizacion = DateTime.Now;

            var salidaDetalle = new SalidaMedicamentoDetalle
            {
                SalidaId = command.SalidaId,
                ConsultorioDestinoId = command.ConsultorioDestinoId,
                UsuarioId = command.UsuarioId,
                LoteId = command.LoteId,
                TipoInsumoId= command.TipoInsumoId,
                TipoMovimientoId = command.TipoMovimientoId,
                FormaFarmaceuticaId = command.FormaFarmaceuticaId,
                TipoEnvaseId = command.TipoEnvaseId,
                Cantidad = command.Cantidad,
                CantidadEnvase = command.CantidadEnvase,
                Observaciones = command.Observaciones,
                FechaCreacion = DateTime.Now
            };

            await _salidaMedicamentoDetalleRepository.AddAsync(salidaDetalle);
            await _salidaMedicamentoDetalleRepository.SaveChangesAsync();

            await _movimientoRepository.AddAsync(new MovimientoInventario
            {
                LoteId = command.LoteId,
                SalidaDetalleId = salidaDetalle.Id,
                UsuarioId = command.UsuarioId,
                TipoMovimiento = "S",
                Origen = "Dispensacion",
                ReferenciaId = command.SalidaId,
                Cantidad = command.Cantidad,
                CantidadEnvase = command.CantidadEnvase,
                CantidadTotal = (command.Cantidad*command.CantidadEnvase),
                ExistenciaAnterior = existenciaAnterior,
                ExistenciaPosterior = lote.CantidadTotal,
                FechaMovimiento = DateTime.Now,
                Observaciones = command.Observaciones
            });

            await _loteRepository.SaveChangesAsync();
            await _movimientoRepository.SaveChangesAsync();

            return new SalidaMedicamentoDetalleDto
            {
                Id = salidaDetalle.Id,
                UsuarioId = salidaDetalle.UsuarioId,
                SalidaId = salidaDetalle.SalidaId,
                ConsultorioDestinoId = salidaDetalle.ConsultorioDestinoId,
                LoteId = salidaDetalle.LoteId,
                TipoInsumoId = salidaDetalle.TipoInsumoId,
                TipoMovimientoId = salidaDetalle.TipoMovimientoId,
                FormaFarmaceuticaId = salidaDetalle.FormaFarmaceuticaId,
                TipoEnvaseId = salidaDetalle.TipoEnvaseId,
                Cantidad = salidaDetalle.Cantidad,
                CantidadEnvase = salidaDetalle.CantidadEnvase,
                Observaciones = salidaDetalle.Observaciones,
                FechaCreacion = DateTime.Now
            };
        }
    }
}
