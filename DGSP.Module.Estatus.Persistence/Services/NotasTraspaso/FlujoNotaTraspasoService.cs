using DGSP.Module.Estatus.Application.Interfaces.NotasTraspaso;
using DGSP.Module.Estatus.Application.Services.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.Estatus.NotasTraspaso;

namespace DGSP.Module.Estatus.Persistence.Services.NotasTraspaso
{
    public class FlujoNotaTraspasoService : IFlujoNotaTraspasoService
    {
        private readonly IFlujoNotaTraspasoRepository _flujoNotaTraspasoRepository;

        public FlujoNotaTraspasoService(IFlujoNotaTraspasoRepository flujoNotaTraspasoRepository)
        {
            _flujoNotaTraspasoRepository = flujoNotaTraspasoRepository;
        }

        public async Task<List<FlujoNotaTraspasoDto>> GetAllFlujosNotasTraspaso()
        {
            var flujo = await _flujoNotaTraspasoRepository.GetAllFlujosNotasTraspaso();

            return flujo.Select(e => new FlujoNotaTraspasoDto
            {
                EstatusId = e.EstatusId,
                ESucesivoId = e.ESucesivoId,
                PermisoId = e.PermisoId,
                Boton = e.Boton,
            }).ToList();
        }

        public async Task<List<FlujoNotaTraspasoDto>> GetEstatusConsecutivos(int estatus)
        {
            var flujo = await _flujoNotaTraspasoRepository.GetEstatusConsecutivos(estatus);

            return flujo.Select(e => new FlujoNotaTraspasoDto
            {
                EstatusId = e.EstatusId,
                ESucesivoId = e.ESucesivoId,
                PermisoId = e.PermisoId,
                Boton = e.Boton,
            }).ToList();
        }
    }
}
