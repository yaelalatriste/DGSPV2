using DGSP.Module.Modulos.Application.Mapping;
using DGSP.Shared.Contracts.DTOs.Modulos;
using Microsoft.EntityFrameworkCore;
using Modulos.Persistence.Database;

namespace DGSP.Module.Modulos.Application.Queries
{
    public interface ISubmodulosQueryService
    {
        Task<List<SubmoduloDto>> GetSubmoduloByModuloAsync(int modulo);
        Task<SubmoduloDto> GetSubmoduloByIdAsync(int submodulo);
    }
    public class SubmodulosQueryService : ISubmodulosQueryService
    {
        private readonly ModulosDbContext _context;
        public SubmodulosQueryService(ModulosDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubmoduloDto>> GetSubmoduloByModuloAsync(int modulo)
        {
            var collection = await _context.Submodulos.Where(sm => sm.ModuloId == modulo).OrderBy(x => x.Id).ToListAsync();

            return collection.MapTo<List<SubmoduloDto>>();
        }
        
        public async Task<SubmoduloDto> GetSubmoduloByIdAsync(int submodulo)
        {
            var collection = await _context.Submodulos.SingleOrDefaultAsync(sm => sm.Id == submodulo);

            return collection.MapTo<SubmoduloDto>();
        }
    }
}
