using DGSP.Module.Estatus.Application.Interfaces.Continuidades;
using DGSP.Module.Estatus.Application.Services.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.Estatus.Continuidades;

namespace DGSP.Module.Estatus.Persistence.Services.Continuidades
{
    public class FlujoEntregableContinuidadService : IFlujoEntregableContinuidadService
    {
        private readonly IFlujoEntregableContinuidadRepository _FlujoEntregableContinuidadRepository;

        public FlujoEntregableContinuidadService(IFlujoEntregableContinuidadRepository FlujoEntregableContinuidadRepository)
        {
            _FlujoEntregableContinuidadRepository = FlujoEntregableContinuidadRepository;
        }

        public async Task<List<FlujoEntregableContinuidadDto>> GetAllFlujosEntregablesContinuidades()
        {
            var flujo = await _FlujoEntregableContinuidadRepository.GetAllFlujosEntregablesContinuidades();

            return flujo.Select(e => new FlujoEntregableContinuidadDto
            {
                EstatusId = e.EstatusId,
                ESucesivoId = e.ESucesivoId,
                EntregableId = e.EntregableId,
                PermisoId = e.PermisoId,
                Editable = e.Editable,
            }).ToList();
        }

        public async Task<List<FlujoEntregableContinuidadDto>> GetEstatusConsecutivos(int estatus)
        {
            var flujo = await _FlujoEntregableContinuidadRepository.GetEstatusConsecutivos(estatus);

            return flujo.Select(e => new FlujoEntregableContinuidadDto
            {
                EstatusId = e.EstatusId,
                ESucesivoId = e.ESucesivoId,
                EntregableId = e.EntregableId,
                PermisoId = e.PermisoId,
                Editable = e.Editable,
            }).ToList();
        }
    }
}
