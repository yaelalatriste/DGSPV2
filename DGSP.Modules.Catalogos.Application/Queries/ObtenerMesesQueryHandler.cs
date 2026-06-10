using DGSP.Components.Common.Interfaces.Catalogos;
using DGSP.Modules.Catalogos.Contract.DTOs;
using DGSP.Modules.Catalogos.Contract.Queries;
using DGSP.Shared.Abstractions.Queries;

namespace DGSP.Modules.Catalogos.Application.Queries
{
    public class ObtenerMesesQueryHandler : IQueryHandler<ObtenerMesesQuery, List<CTMesDto>>
    {
        private readonly ICTMesRepository _meses;

        public ObtenerMesesQueryHandler(ICTMesRepository meses)
        {
            _meses = meses;
        }

        public async Task<List<CTMesDto>> Handle(ObtenerMesesQuery request, CancellationToken cancellationToken)
        {
            var meses = await _meses.GetAllMeses();

            var result = meses.Select(m => new CTMesDto 
            { 
                Id = m.Id,
                Nombre = m.Nombre,
            }).ToList();

            return result;
        }
    }
}
