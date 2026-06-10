using DGSP.Module.Catalogos.Application.Interfaces.SMedicos;
using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Catalogos.Persistence.Services.SMedicos
{
    public class CTUnidadService : ICTUnidadService
    {
        private readonly ICTUnidadRepository _cTUnidadRepository;

        public CTUnidadService(ICTUnidadRepository cTUnidadRepository)
        {
            _cTUnidadRepository = cTUnidadRepository;
        }

        public async Task<List<CTUnidadDto>> GetAllUnidadesAsync()
        {
            var unidades = await _cTUnidadRepository.GetAllUnidadesAsync();

            return unidades.Select(u => new CTUnidadDto {
                Id = u.Id,
                Nombre = u.Nombre,
                Descripcion = u.Descripcion,
                Abreviacion = u.Abreviacion,
            }).ToList();
        }

        public async Task<CTUnidadDto> GetUnidadByIdAsync(int id)
        {
            var u = await _cTUnidadRepository.GetUnidadByIdAsync(id);

            return new CTUnidadDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Descripcion = u.Descripcion,
                Abreviacion = u.Abreviacion,
            };
        }
    }
}
