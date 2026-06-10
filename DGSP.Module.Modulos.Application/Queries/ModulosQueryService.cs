using DGSP.Module.Modulos.Application.Mapping;
using DGSP.Shared.Contracts.DTOs.Modulos;
using Microsoft.EntityFrameworkCore;
using Modulos.Persistence.Database;

namespace DGSP.Module.Modulos.Application.Queries
{
    public interface IModulosQueryService
    {
        Task<List<ModuloDto>> GetAllModulosAsync();
        Task<ModuloDto> GetModuloByIdAsync(int modulo);
    }
    public class ModulosQueryService : IModulosQueryService
    {
        private readonly ModulosDbContext _context;
        public ModulosQueryService(ModulosDbContext context)
        {
            _context = context;
        }

        public async Task<List<ModuloDto>> GetAllModulosAsync()
        {
            var collection = await _context.Modulos
                .OrderBy(x => x.Id).ToListAsync();

            return collection.MapTo<List<ModuloDto>>();
        }

        public async Task<ModuloDto> GetModuloByIdAsync(int modulo)
        {
            return (await _context.Modulos.SingleAsync(x => x.Id == modulo)).MapTo<ModuloDto>();
        }
    }
}
