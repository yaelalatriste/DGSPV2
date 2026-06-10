using DGSP.Module.Modulos.Application.Mapping;
using DGSP.Shared.Contracts.DTOs.Modulos;
using Microsoft.EntityFrameworkCore;
using Modulos.Persistence.Database;

namespace DGSP.Module.Modulos.Application.Queries
{
    public interface IOpcionesQueryService
    {
        Task<List<OpcionDto>> GetOpcionesBySubmoduloAsync(int submodulo);
        Task<OpcionDto> GetOpcionByIdAsync(int opcion);
    }
    public class OpcionesQueryService : IOpcionesQueryService
    {
        private readonly ModulosDbContext _context;
        public OpcionesQueryService(ModulosDbContext context)
        {
            _context = context;
        }

        public async Task<List<OpcionDto>> GetOpcionesBySubmoduloAsync(int submodulo)
        {
            var collection = await _context.Opciones.Where(sm => sm.SubmoduloId == submodulo).OrderBy(x => x.Orden).ToListAsync();

            return collection.MapTo<List<OpcionDto>>();
        }
        
        public async Task<OpcionDto> GetOpcionByIdAsync(int opcion)
        {
            var collection = await _context.Opciones.SingleOrDefaultAsync(sm => sm.Id == opcion);

            return collection.MapTo<OpcionDto>();
        }
    }
}
