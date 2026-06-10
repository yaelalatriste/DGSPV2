using DGSP.Module.Estatus.Application.Interfaces.Continuidades;
using DGSP.Module.Estatus.Application.Services.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.Estatus.Continuidades;

namespace DGSP.Module.Estatus.Persistence.Services.Continuidades
{
    public class FlujoContinuidadService : IFlujoContinuidadService
    {
        private readonly IFlujoContinuidadRepository _flujoContinuidadRepository;

        public FlujoContinuidadService(IFlujoContinuidadRepository flujoContinuidadRepository)
        {
            _flujoContinuidadRepository = flujoContinuidadRepository;
        }

        public async Task<List<FlujoContinuidadDto>> GetAllFlujosContinuidades()
        {
            var flujo = await _flujoContinuidadRepository.GetAllFlujosContinuidades();

            return flujo.Select(e => new FlujoContinuidadDto
            {
                EstatusId = e.EstatusId,
                ESucesivoId = e.ESucesivoId,
                PermisoId = e.PermisoId,
                Boton = e.Boton,
            }).ToList();
        }

        public async Task<List<FlujoContinuidadDto>> GetEstatusConsecutivos(int estatus)
        {
            var flujo = await _flujoContinuidadRepository.GetEstatusConsecutivos(estatus);

            return flujo.Select(e => new FlujoContinuidadDto
            {
                EstatusId = e.EstatusId,
                ESucesivoId = e.ESucesivoId,
                PermisoId = e.PermisoId,
                Boton = e.Boton,
            }).ToList();
        }
    }
}
