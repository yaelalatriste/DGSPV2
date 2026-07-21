using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.OficiosContinuidades;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;

namespace DGSP.Module.Seguros.Application.Services.DGSP.Continuidades
{
    public interface IOficioContinuidadService
    {
        Task<List<OficioContinuidadDto>> GetOficiosByContinuidadAsync(int continuidadId);
        Task<OficioContinuidadDto> RegistrarOficioContinuidadService(RegistrarOficioContinuidadCommand command);
    }
}
