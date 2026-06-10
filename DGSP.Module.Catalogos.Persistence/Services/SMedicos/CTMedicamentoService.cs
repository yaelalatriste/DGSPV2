using DGSP.Module.Catalogos.Application.Interfaces.SMedicos;
using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Module.Catalogos.Domain.SMedicos;
using DGSP.Shared.Contracts.Commands.Catalogos.SMedicos.Medicamentos;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DGSP.Module.Catalogos.Persistence.Services.SMedicos
{
    public class CTMedicamentoService : ICTMedicamentoService
    {
        private readonly ICTMedicamentoRepository _cTMedicamentoRepository;

        public CTMedicamentoService(ICTMedicamentoRepository cTMedicamentoRepository)
        {
            _cTMedicamentoRepository = cTMedicamentoRepository;
        }

        public async Task<List<CTMedicamentoDto>> GetAllMedicamentosAsync()
        {
            var medicamentos = await _cTMedicamentoRepository.GetAllMedicamentosAsync();

            return medicamentos.Select(m => new CTMedicamentoDto {
                Id = m.Id,
                Anio = m.Anio,
                UsuarioId = m.UsuarioId,
                TipoInsumoId = m.TipoInsumoId,
                Formula = m.Formula,
                Nombre = m.Nombre,
                Presentacion = m.Presentacion,
                TipoEnvaseId = m.TipoEnvaseId,
                FechaCreacion = m.FechaCreacion,
                FechaActualizacion = m.FechaActualizacion
            }).ToList();
        }

        public async Task<CTMedicamentoDto> GetMedicamentoByIdAsync(int id)
        {
            var data = await _cTMedicamentoRepository.GetMedicamentoByIdAsync(id);

            return new CTMedicamentoDto {
                Id = data.Id,
                Anio = data.Anio,
                UsuarioId = data.UsuarioId,
                TipoInsumoId = data.TipoInsumoId,
                Formula = data.Formula,
                Nombre = data.Nombre,
                Presentacion = data.Presentacion,
                TipoEnvaseId = data.TipoEnvaseId,
                FechaCreacion = data.FechaCreacion,
                FechaActualizacion = data.FechaActualizacion
            };
        }

        public async Task<CTMedicamentoDto> RegistrarMedicamentoAsync(RegistrarCTMedicamentoCommand command)
        {
            var medicamento = new CTMedicamento
            {
                Anio = DateTime.Now.Year,
                UsuarioId = command.UsuarioId,
                TipoInsumoId = command.TipoInsumoId,
                Formula = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(command.Formula.ToLower()),
                Nombre = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(command.Nombre.ToLower()),
                Presentacion = command.Presentacion,
                TipoEnvaseId = command.TipoEnvaseId,
                FechaCreacion = DateTime.Now
            };

            await _cTMedicamentoRepository.RegistrarMedicamentoAsync(medicamento);
            await _cTMedicamentoRepository.SaveChangesAsync();

            return new CTMedicamentoDto
            {
                Id = medicamento.Id,
                TipoInsumoId = medicamento.TipoInsumoId,
                Formula = medicamento.Formula,
                Nombre = medicamento.Nombre,
                Presentacion = medicamento.Presentacion,
                TipoEnvaseId = medicamento.TipoEnvaseId,
                FechaCreacion = medicamento.FechaCreacion,
                FechaActualizacion = medicamento.FechaActualizacion,
            };
        }

        public async Task<CTMedicamentoDto> ActualizarMedicamentoAsync(ActualizarCTMedicamentoCommand command)
        {
            var medicamento = await _cTMedicamentoRepository.GetMedicamentoByIdAsync(command.Id);

            medicamento.Anio = DateTime.Now.Year;
            medicamento.UsuarioId = command.UsuarioId;
            medicamento.TipoInsumoId = command.TipoInsumoId;
            medicamento.Formula = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(command.Formula.ToLower());
            medicamento.Nombre = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(command.Nombre.ToLower());
            medicamento.Presentacion = command.Presentacion;
            medicamento.TipoEnvaseId = command.TipoEnvaseId;
            medicamento.FechaActualizacion = DateTime.Now;

            await _cTMedicamentoRepository.ActualizarMedicamentoAsync(medicamento);

            return new CTMedicamentoDto
            {
                Id = medicamento.Id,
                Anio = medicamento.Anio,
                UsuarioId = medicamento.UsuarioId,
                TipoInsumoId = medicamento.TipoInsumoId,
                Formula = medicamento.Formula,
                Nombre = medicamento.Nombre,
                Presentacion = medicamento.Presentacion,
                TipoEnvaseId = medicamento.TipoEnvaseId,
                FechaCreacion = medicamento.FechaCreacion,
                FechaActualizacion = medicamento.FechaActualizacion
            };
        }

        public async Task<List<CTMedicamentoDto>> GetMedicamentosByAnioAsync(int anio)
        {
            var medicamentos = await _cTMedicamentoRepository.GetAllMedicamentosAsync();

            return medicamentos.Where(m => m.Anio == anio).Select(m => new CTMedicamentoDto
            {
                Id = m.Id,
                Anio = m.Anio,
                UsuarioId = m.UsuarioId,
                TipoInsumoId = m.TipoInsumoId,
                Formula = m.Formula,
                Nombre = m.Nombre,
                Presentacion = m.Presentacion,
                TipoEnvaseId = m.TipoEnvaseId,
                FechaCreacion = m.FechaCreacion,
                FechaActualizacion = m.FechaActualizacion
            }).ToList();
        }
    }
}
