using DGSP.Module.SMedicos.Application.Interfaces.Siacom.TipoConsultaDetalle;
using DGSP.Module.SMedicos.Persistence;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Catalogos;
using Microsoft.EntityFrameworkCore;
using SMedicos.Service.Queries.Mapping;

namespace SMedicos.Services.Queries.Queries.Siacom.TipoConsultaDetalle
{
    public class TCDetalleQueryService : ITCDetalleQueryService
    {
        private readonly SiacomDbContext _context;

        public TCDetalleQueryService(SiacomDbContext context)
        {
            _context = context;
        }

        public async Task<List<CTTipoConsultaDetalleDto>> GetAllTiposConsultaDetalle()
        {
            var consultorios = await _context.CTTipoConsultasDetalle.ToListAsync();

            return consultorios.MapTo<List<CTTipoConsultaDetalleDto>>();
        }

        public async Task<CTTipoConsultaDetalleDto> GetTipoConsultaDetalleById(int id)
        {
            var consultorios = await _context.CTTipoConsultasDetalle.Where(c => c.FiIdTipoConsultaDetalle == id).FirstAsync();

            return consultorios.MapTo<CTTipoConsultaDetalleDto>();
        }
    }
}
