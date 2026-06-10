using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.OficiosContinuidades;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;

namespace DGSP.Module.Seguros.Application.Services.DGSP.Continuidades
{
    public interface IOficioContinuidadService
    {
        Task<List<OficioContinuidadDto>> GetOficiosByContinuidadAsync(int continuidadId);
        Task<OficioContinuidadDto> RegistrarOficioContinuidadService(RegistrarOficioContinuidadCommand command);
    }
}
