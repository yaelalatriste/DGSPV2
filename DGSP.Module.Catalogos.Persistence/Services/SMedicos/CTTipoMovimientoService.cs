using DGSP.Module.Catalogos.Application.Interfaces.SMedicos;
using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Catalogos.Persistence.Services.SMedicos
{
    public class CTTipoMovimientoService : ICTTipoMovimientoService
    {
        private readonly ICTTipoMovimientoRepository _cTTipoMovimientoRepository;

        public CTTipoMovimientoService(ICTTipoMovimientoRepository cTTipoMovimientoRepository)
        {
            _cTTipoMovimientoRepository = cTTipoMovimientoRepository;
        }

        public async Task<List<CTTipoMovimientoDto>> GetAllTiposMovimientosAsync()
        {
            var tiposMovimiento = await _cTTipoMovimientoRepository.GetAllTiposMovimientosAsync();

            return tiposMovimiento.Select(tm => new CTTipoMovimientoDto {
                Id = tm.Id,
                Nombre = tm.Nombre,
                Entrada = tm.Entrada,
                Salida = tm.Salida,
                Activo = tm.Activo,
            }).ToList();
        }

        public async Task<CTTipoMovimientoDto> GetTipoMovimientoByIdAsync(int id)
        {
            var data = await _cTTipoMovimientoRepository.GetTipoMovimientoByIdAsync(id);

            return new CTTipoMovimientoDto
            {
                Id = data.Id,
                Nombre = data.Nombre,
                Entrada = data.Entrada,
                Salida = data.Salida,
                Activo = data.Activo,
            };
        }
        
        public async Task<List<CTTipoMovimientoDto>> GetMovimientosEntradaAsync()
        {
            var tiposMovimiento = await _cTTipoMovimientoRepository.GetMovimientosEntradaAsync();

            return tiposMovimiento.Select(tm => new CTTipoMovimientoDto
            {
                Id = tm.Id,
                Nombre = tm.Nombre,
                Entrada = tm.Entrada,
                Salida = tm.Salida,
                Activo = tm.Activo,
            }).ToList();
        }
        
        public async Task<List<CTTipoMovimientoDto>> GetMovimientosSalidaAsync()
        {
            var tiposMovimiento = await _cTTipoMovimientoRepository.GetMovimientosSalidaAsync();

            return tiposMovimiento.Select(tm => new CTTipoMovimientoDto
            {
                Id = tm.Id,
                Nombre = tm.Nombre,
                Entrada = tm.Entrada,
                Salida = tm.Salida,
                Activo = tm.Activo,
            }).ToList();
        }
    }
}
