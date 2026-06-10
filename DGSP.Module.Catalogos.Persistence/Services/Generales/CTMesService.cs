using DGSP.Module.Catalogos.Application.Interfaces.Generales;
using DGSP.Module.Catalogos.Application.Services.Generales;
using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;

namespace DGSP.Module.Catalogos.Persistence.Services.Generales
{

    public class CTMesService : ICTMesService
    {
        private readonly ICTMesRepository _cTMesRepository;

        public CTMesService(ICTMesRepository cTMesRepository)
        {
            _cTMesRepository = cTMesRepository;
        }

        public async Task<List<CTMesDto>> GetAllMesesAsync()
        {
            var meses = await _cTMesRepository.GetAllMesesAsync();

            return meses.Select(m => new CTMesDto { 
                Id = m.Id,
                Nombre = m.Nombre,
            }).ToList();
        }

        public async Task<CTMesDto> GetMesByIdAsync(int id)
        {
            var mes = await _cTMesRepository.GetMesByIdAsync(id);

            return new CTMesDto {
                Id = mes.Id,
                Nombre = mes.Nombre,
            };
        }
    }
}
