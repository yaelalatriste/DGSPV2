using DGSP.Module.Catalogos.Application.Interfaces.Generales;
using DGSP.Module.Catalogos.Application.Services.Generales;
using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;

namespace DGSP.Module.Catalogos.Persistence.Services.Generales
{

    public class CTAreaService : ICTAreaService
    {
        private readonly ICTAreaRepository _cTAreaRepository;

        public CTAreaService(ICTAreaRepository cTAreaRepository)
        {
            _cTAreaRepository = cTAreaRepository;
        }

        public async Task<List<CTAreaDto>> GetAllAreasAsync()
        {
            var areas = await _cTAreaRepository.GetAllAreasAsync();

            return areas.Select(a => new CTAreaDto { 
                Id = a.Id,
                Folios = a.Folios,
                Nombre = a.Nombre,
                Abreviacion = a.Abreviacion,
                Descripcion = a.Descripcion,
                Responsable = a.Responsable,
                Correo = a.Correo,
                Icono = a.Icono,
                Fondo = a.Fondo,
                FondoHexadecimal = a.FondoHexadecimal,
            }).ToList();
        }

        public async Task<CTAreaDto> GetAreaByIdAsync(int id)
        {
            var area = await _cTAreaRepository.GetAreaByIdAsync(id);

            return new CTAreaDto {
                Id = area.Id,
                Folios = area.Folios,
                Nombre = area.Nombre,
                Abreviacion = area.Abreviacion,
                Descripcion = area.Descripcion,
                Responsable = area.Responsable,
                Correo = area.Correo,
                Icono = area.Icono,
                Fondo = area.Fondo,
                FondoHexadecimal = area.FondoHexadecimal,
            };
        }
    }
}
