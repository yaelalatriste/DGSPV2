using DGSP.Module.Catalogos.Application.Interfaces.SMedicos;
using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;

namespace DGSP.Module.Catalogos.Persistence.Services.SMedicos
{
    public class CTTipoInsumoService : ICTTipoInsumoService
    {
        private readonly ICTTipoInsumoRepository _cTTipoInsumoRepository;

        public CTTipoInsumoService(ICTTipoInsumoRepository cTTipoInsumoRepository)
        {
            _cTTipoInsumoRepository = cTTipoInsumoRepository;
        }

        public async Task<List<CTTipoInsumoDto>> GetAllTiposInsumosAsync()
        {
            var tiposInsumo = await _cTTipoInsumoRepository.GetAllTiposInsumosAsync();
            
            return tiposInsumo.Select(ti => new CTTipoInsumoDto {
                Id = ti.Id,
                Nombre = ti.Nombre,
                Activo = ti.Activo
            }).ToList();
        }

        public async Task<CTTipoInsumoDto> GetTipoInsumoByIdAsync(int id)
        {
            var ti = await _cTTipoInsumoRepository.GetTipoInsumoByIdAsync(id);

            return new CTTipoInsumoDto
            {
                Id = ti.Id,
                Nombre = ti.Nombre,
                Activo = ti.Activo
            };
        }

    }
}
