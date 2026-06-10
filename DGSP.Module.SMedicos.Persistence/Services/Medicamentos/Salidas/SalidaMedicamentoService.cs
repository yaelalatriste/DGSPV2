using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Entradas;
using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Movimientos;
using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Salidas;
using DGSP.Module.SMedicos.Application.Services.Medicamentos.Salidas;
using DGSP.Module.SMedicos.Domain.Inventarios;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Salidas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Salidas;

namespace DGSP.Module.SMedicos.Persistence.Services.Medicamentos.Salidas
{
    public class SalidaMedicamentoService : ISalidaMedicamentoService
    {
        private readonly SMedicosDbContext _db;
        private readonly ISalidaMedicamentoRepository _salidaRepository;
        private readonly IMovimientoInventarioRepository _movimientoRepository;
        private readonly ILoteMedicamentoRepository _loteRepository;

        public SalidaMedicamentoService(SMedicosDbContext db, ISalidaMedicamentoRepository salidaRepository, ISalidaMedicamentoDetalleRepository detalleRepository,
            IMovimientoInventarioRepository movimientoRepository, ILoteMedicamentoRepository loteRepository)
        {
            _db = db;
            _salidaRepository = salidaRepository;
            _movimientoRepository = movimientoRepository;
            _loteRepository = loteRepository;
        }

        public async Task<List<SalidaMedicamentoDto>> GetAllSalidasAsync()
        {
            var data = await _salidaRepository.GetAllSalidasMedicamentosAsync();

            return data.Select(s => new SalidaMedicamentoDto
            {
                Id = s.Id,
                UsuarioId = s.UsuarioId,
                ConsultaId = s.ConsultaId,
                ConsultorioId = s.ConsultorioId,
                FechaSalida = s.FechaSalida,
                Observaciones = s.Observaciones,
            }).ToList();
        }

        public async Task<SalidaMedicamentoDto> ObtenerPorIdAsync(int salidaId)
        {
            var salida = await _salidaRepository.GetByIdAsync(salidaId);

            return new SalidaMedicamentoDto
            {
                Id = salida.Id,
                UsuarioId = salida.UsuarioId,
                ConsultaId = salida.ConsultaId,
                ConsultorioId = salida.ConsultorioId,
                FechaSalida = salida.FechaSalida,
                Observaciones = salida.Observaciones,
            };
        }

        public async Task<SalidaMedicamentoDto> RegistrarSalidaMedicamentoAsync(RegistrarSalidaMedicamentoCommand command)
        {
            var salida = new SalidaMedicamento
            {
                ConsultaId = command.ConsultaId,
                UsuarioId = command.UsuarioId,
                ConsultorioId = command.ConsultorioId,
                FechaSalida = DateTime.Now,
                Observaciones = command.Observaciones,
            };

            await _salidaRepository.AddAsync(salida);
            await _salidaRepository.SaveChangesAsync();

            return new SalidaMedicamentoDto {
                Id = salida.Id,
                UsuarioId = salida.UsuarioId,
                ConsultaId = salida.ConsultaId,
                ConsultorioId = salida.ConsultorioId,
                FechaSalida = salida.FechaSalida,
                Observaciones = salida.Observaciones
            };
        }
    }
}
