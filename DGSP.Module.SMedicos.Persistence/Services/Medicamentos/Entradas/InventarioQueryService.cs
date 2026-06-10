using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Entradas;
using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Movimientos;
using DGSP.Module.SMedicos.Application.Services.Medicamentos.Entradas;
using DGSP.Module.SMedicos.Domain.Inventarios;
using DGSP.Module.SMedicos.Domain.Movimientos;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;

namespace DGSP.Module.SMedicos.Persistence.Services.Medicamentos.Entradas
{
    public class InventarioService : IInventarioAppService
    {
        private readonly ILoteMedicamentoRepository _loteRepository;
        private readonly IMovimientoInventarioRepository _movimientoRepository;

        public InventarioService(ILoteMedicamentoRepository loteRepository, IMovimientoInventarioRepository movimientoRepository)
        {
            _loteRepository = loteRepository;
            _movimientoRepository = movimientoRepository;
        }

        public async Task<LoteDto> RegistrarLoteAsync(RegistrarLoteMedicamentoCommand command)
        {
            var existe = await _loteRepository.ExistsAsync(command.ConsultorioId, command.MedicamentoId, command.Lote, command.FechaCaducidad);
            var fecha = DateTime.Now;
            if (existe)
            {
                var loteExistente = await _loteRepository.GetLoteExistAsync(command.ConsultorioId, command.MedicamentoId, command.Lote, command.FechaCaducidad);

                var existenciaAnterior = loteExistente.CantidadTotal;

                loteExistente.Cantidad += command.Cantidad;

                if (command.CantidadEnvase != 0)
                    loteExistente.CantidadEnvase = loteExistente.CantidadEnvase + command.CantidadEnvase;

                loteExistente.CantidadTotal += command.CantidadTotal;
                loteExistente.FechaActualizacion = DateTime.Now;

                await _movimientoRepository.AddAsync(new MovimientoInventario
                {
                    LoteId = loteExistente.Id,
                    UsuarioId = loteExistente.UsuarioId,
                    TipoMovimiento = "E",
                    Origen = "Entrada Adicional",
                    ReferenciaId = loteExistente.Id,
                    Cantidad = loteExistente.Cantidad,
                    CantidadEnvase = command.CantidadEnvase,
                    CantidadTotal = loteExistente.CantidadTotal,
                    ExistenciaAnterior = existenciaAnterior,
                    ExistenciaPosterior = loteExistente.CantidadTotal,
                    FechaMovimiento = DateTime.Now,
                });

                await _loteRepository.SaveChangesAsync();

                return new LoteDto
                {
                    Id = loteExistente.Id,
                    UsuarioId = loteExistente.UsuarioId,
                    ConsultorioId = loteExistente.ConsultorioId,
                    TipoInsumoId = command.TipoInsumoId,
                    TipoMovimientoId = command.TipoMovimientoId,
                    MedicamentoId = loteExistente.MedicamentoId,
                    FormaFarmaceuticaId = loteExistente.FormaFarmaceuticaId,
                    TipoEnvaseId = loteExistente.TipoEnvaseId,
                    UnidadContenidoId = loteExistente.UnidadContenidoId,
                    Lote = loteExistente.Lote,
                    FechaCaducidad = loteExistente.FechaCaducidad,
                    Cantidad = loteExistente.Cantidad,
                    CantidadEnvase = loteExistente.CantidadEnvase,
                    CantidadTotal = loteExistente.CantidadTotal,
                    Concentracion = loteExistente.Concentracion,
                    Observaciones = loteExistente.Observaciones,
                };

            }
            else
            {
                var entity = new LoteMedicamento
                {
                    UsuarioId = command.UsuarioId,
                    ConsultorioId = command.ConsultorioId,
                    TipoInsumoId = command.TipoInsumoId,
                    TipoMovimientoId = command.TipoMovimientoId,
                    MedicamentoId = command.MedicamentoId,
                    FormaFarmaceuticaId = command.FormaFarmaceuticaId,
                    TipoEnvaseId = command.TipoEnvaseId,
                    Lote = command.Lote,
                    FechaCaducidad = command.FechaCaducidad,
                    Cantidad = command.Cantidad,
                    CantidadEnvase = command.CantidadEnvase,
                    CantidadTotal = command.CantidadTotal,
                    Concentracion = command.Concentracion,
                    UnidadContenidoId = command.UnidadContenidoId,
                    Observaciones = command.Observaciones,
                    FechaCreacion = fecha
                };

                await _loteRepository.AddAsync(entity);
                await _loteRepository.SaveChangesAsync();

                await _movimientoRepository.AddAsync(new MovimientoInventario
                {
                    UsuarioId = entity.UsuarioId,
                    LoteId = entity.Id,
                    TipoMovimiento = "E",
                    Origen = "Registro de Lote",
                    ReferenciaId = entity.Id,
                    Cantidad = command.Cantidad,
                    CantidadEnvase = command.CantidadEnvase,
                    CantidadTotal = command.CantidadTotal,
                    ExistenciaAnterior = 0,
                    ExistenciaPosterior = command.CantidadTotal,
                    FechaMovimiento = fecha,
                    Observaciones = "Alta inicial de lote"
                });

                await _loteRepository.SaveChangesAsync();

                return new LoteDto
                {
                    Id = entity.Id,
                    UsuarioId = entity.UsuarioId,
                    ConsultorioId = entity.ConsultorioId,
                    MedicamentoId = entity.MedicamentoId,
                    TipoInsumoId = entity.TipoInsumoId,
                    TipoMovimientoId = entity.TipoMovimientoId,
                    Lote = entity.Lote,
                    FechaCaducidad = entity.FechaCaducidad,
                    FormaFarmaceuticaId = entity.FormaFarmaceuticaId,
                    TipoEnvaseId = entity.TipoEnvaseId,
                    UnidadContenidoId = entity.UnidadContenidoId,
                    Cantidad = entity.Cantidad,
                    CantidadEnvase = entity.CantidadEnvase,
                    CantidadTotal = entity.CantidadTotal,
                    Concentracion = entity.Concentracion,
                    Observaciones = entity.Observaciones,
                };
            }
        }

        public async Task<List<LoteDto>> ConsultarLotesAsync()
        {
            var data = await _loteRepository.GetAllAsync();

            return data.Select(x => new LoteDto
            {
                Id = x.Id,
                UsuarioId = x.UsuarioId,
                ConsultorioId = x.ConsultorioId,
                MedicamentoId = x.MedicamentoId,
                TipoInsumoId = x.TipoInsumoId,
                TipoMovimientoId = x.TipoMovimientoId,
                Lote = x.Lote,
                FechaCaducidad = x.FechaCaducidad,
                FormaFarmaceuticaId = x.FormaFarmaceuticaId,
                TipoEnvaseId = x.TipoEnvaseId,
                UnidadContenidoId = x.UnidadContenidoId,
                Cantidad = x.Cantidad,
                CantidadEnvase = x.CantidadEnvase,
                CantidadTotal = x.CantidadTotal,
                Concentracion = x.Concentracion,
                Observaciones = x.Observaciones,
            }).ToList();
        }

        public async Task<LoteDto> GetLoteByIdAsync(int id)
        {
            var lote = await _loteRepository.GetByIdAsync(id);
            LoteDto loteDto = new LoteDto
            {
                Id = lote.Id,
                UsuarioId = lote.UsuarioId,
                ConsultorioId = lote.ConsultorioId,
                MedicamentoId = lote.MedicamentoId,
                TipoInsumoId = lote.TipoInsumoId,
                TipoMovimientoId = lote.TipoMovimientoId,
                Lote = lote.Lote,
                FechaCaducidad = lote.FechaCaducidad,
                FormaFarmaceuticaId = lote.FormaFarmaceuticaId,
                TipoEnvaseId = lote.TipoEnvaseId,
                Cantidad = lote.Cantidad,
                CantidadEnvase = lote.CantidadEnvase,
                CantidadTotal = lote.CantidadTotal,
                Concentracion = lote.Concentracion,
                UnidadContenidoId = lote.UnidadContenidoId,
                Observaciones = lote.Observaciones,
            };

            return loteDto;
        }

        public async Task<LoteDto> GetDatosByLoteAsync(string lote)
        {
            var lot = await _loteRepository.GetDatosByLoteAsync(lote);

            LoteDto loteDto = new LoteDto
            {
                Id = lot.Id,
                UsuarioId = lot.UsuarioId,
                ConsultorioId = lot.ConsultorioId,
                TipoInsumoId = lot.TipoInsumoId,
                TipoMovimientoId = lot.TipoMovimientoId,
                MedicamentoId = lot.MedicamentoId,
                Lote = lot.Lote,
                FechaCaducidad = lot.FechaCaducidad,
                FormaFarmaceuticaId = lot.FormaFarmaceuticaId,
                TipoEnvaseId = lot.TipoEnvaseId,
                Cantidad = lot.Cantidad,
                CantidadEnvase = lot.CantidadEnvase,
                CantidadTotal = lot.CantidadTotal,
                Concentracion = lot.Concentracion,
                UnidadContenidoId = lot.UnidadContenidoId,
                Observaciones = lot.Observaciones,
            };

            return loteDto;
        }
        public async Task<List<LoteDto>> GetMedicamentosByLoteConsultorioAsync(string lote, int consultorio)
        {
            var lotes = await _loteRepository.GetMedicamentosByLoteConsultorioAsync(lote, consultorio);

             return lotes.Select(lot => new LoteDto
             {
                Id = lot.Id,
                UsuarioId = lot.UsuarioId,
                ConsultorioId = lot.ConsultorioId,
                TipoInsumoId = lot.TipoInsumoId,
                TipoMovimientoId = lot.TipoMovimientoId,
                MedicamentoId = lot.MedicamentoId,
                Lote = lot.Lote,
                FechaCaducidad = lot.FechaCaducidad,
                FormaFarmaceuticaId = lot.FormaFarmaceuticaId,
                TipoEnvaseId = lot.TipoEnvaseId,
                Cantidad = lot.Cantidad,
                CantidadEnvase = lot.CantidadEnvase,
                CantidadTotal = lot.CantidadTotal,
                Concentracion = lot.Concentracion,
                UnidadContenidoId = lot.UnidadContenidoId,
                Observaciones = lot.Observaciones,
            }).ToList();
        }
        
        public async Task<LoteDto> GetDatosByLoteConsultorioAsync(string lote, int consultorio)
        {
            var lot = await _loteRepository.GetDatosByLoteConsultorioAsync(lote, consultorio);

            LoteDto loteDto = new LoteDto
            {
                Id = lot.Id,
                UsuarioId = lot.UsuarioId,
                ConsultorioId = lot.ConsultorioId,
                TipoInsumoId = lot.TipoInsumoId,
                TipoMovimientoId = lot.TipoMovimientoId,
                MedicamentoId = lot.MedicamentoId,
                Lote = lot.Lote,
                FechaCaducidad = lot.FechaCaducidad,
                FormaFarmaceuticaId = lot.FormaFarmaceuticaId,
                TipoEnvaseId = lot.TipoEnvaseId,
                Cantidad = lot.Cantidad,
                CantidadEnvase = lot.CantidadEnvase,
                CantidadTotal = lot.CantidadTotal,
                Concentracion = lot.Concentracion,
                UnidadContenidoId = lot.UnidadContenidoId,
                Observaciones = lot.Observaciones,
            };

            return loteDto;
        }

        public async Task<LoteDto> GetDatosByLoteConsultorioMedicamentoAsync(string lote, int consultorio, int medicamento)
        {
            var lot = await _loteRepository.GetDatosByLoteConsultorioMedicamentoAsync(lote, consultorio, medicamento);

            LoteDto loteDto = new LoteDto
            {
                Id = lot.Id,
                UsuarioId = lot.UsuarioId,
                ConsultorioId = lot.ConsultorioId,
                TipoInsumoId = lot.TipoInsumoId,
                TipoMovimientoId = lot.TipoMovimientoId,
                MedicamentoId = lot.MedicamentoId,
                Lote = lot.Lote,
                FechaCaducidad = lot.FechaCaducidad,
                FormaFarmaceuticaId = lot.FormaFarmaceuticaId,
                TipoEnvaseId = lot.TipoEnvaseId,
                Cantidad = lot.Cantidad,
                CantidadEnvase = lot.CantidadEnvase,
                CantidadTotal = lot.CantidadTotal,
                Concentracion = lot.Concentracion,
                UnidadContenidoId = lot.UnidadContenidoId,
                Observaciones = lot.Observaciones,
            };

            return loteDto;
        }
    }
}
