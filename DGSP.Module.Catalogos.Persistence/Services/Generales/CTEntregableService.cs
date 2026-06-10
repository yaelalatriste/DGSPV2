using DGSP.Module.Catalogos.Application.Interfaces.Generales;
using DGSP.Module.Catalogos.Application.Services.Generales;
using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;

namespace DGSP.Module.Catalogos.Persistence.Services.Generales
{

    public class CTEntregableService : ICTEntregableService
    {
        private readonly ICTEntregableRepository _cTEntregableRepository;

        public CTEntregableService(ICTEntregableRepository cTEntregableRepository)
        {
            _cTEntregableRepository = cTEntregableRepository;
        }

        public async Task<List<CTEntregableDto>> GetAllEntregablesAsync()
        {
            var Entregables = await _cTEntregableRepository.GetAllEntregablesAsync();

            return Entregables.Select(a => new CTEntregableDto { 
                Id = a.Id,
                Abreviacion = a.Abreviacion,
                Nombre = a.Nombre,
                Descripcion = a.Descripcion,
                FechaCreacion = a.FechaCreacion,
            }).ToList();
        }

        public async Task<CTEntregableDto> GetEntregableByIdAsync(int id)
        {
            var Entregable = await _cTEntregableRepository.GetEntregableByIdAsync(id);

            return new CTEntregableDto {
                Id = Entregable.Id,
                Abreviacion = Entregable.Abreviacion,
                Nombre = Entregable.Nombre,
                Descripcion = Entregable.Descripcion,
                FechaCreacion = Entregable.FechaCreacion,
            };
        }
    }
}
