using DGSP.Module.SMedicos.Application.Interfaces.Siacom.TipoServicios;
using DGSP.Module.SMedicos.Persistence;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Catalogos;
using Microsoft.EntityFrameworkCore;
using SMedicos.Service.Queries.Mapping;

namespace SMedicos.Services.Queries.Queries.Siacom.TiposServicios
{
    public class TipoServicioQueryService : ITipoServicioQueryService
    {
        private readonly SiacomDbContext _context;

        public TipoServicioQueryService(SiacomDbContext context)
        {
            _context = context;
        }

        public async Task<List<CTTipoServicioDto>> GetAllTiposServicios()
        {
            var consultorios = await _context.CTTiposServicios.ToListAsync();

            return consultorios.MapTo<List<CTTipoServicioDto>>();
        }

        public async Task<CTTipoServicioDto> GetTipoServicioById(int id)
        {
            var consultorios = await _context.CTTiposServicios.Where(c => c.FiIdTipoServicio == id).FirstAsync();

            return consultorios.MapTo<CTTipoServicioDto>();
        }
    }
}
