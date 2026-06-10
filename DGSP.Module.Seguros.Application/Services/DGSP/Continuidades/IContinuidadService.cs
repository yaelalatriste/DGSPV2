using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.Continuidad;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;

namespace DGSP.Module.Seguros.Application.Services.DGSP.Continuidades
{
    public interface IContinuidadService
    {
        Task<List<ContinuidadDto>> GetAllContinuidadesAsync();
        Task<List<ContinuidadDto>> GetContinuidadesByAnio(int anio);        
        Task<ContinuidadDto> GetContinuidadByIdAsync(int id);
        Task<List<ContinuidadDto>> GetContinuidadesByEstatus(int estatus);
        Task<ContinuidadDto> RegistrarContinuidadAsync(RegistrarContinuidadCommand command);
        Task<ContinuidadDto> ActualizarContinuidadAsync(ActualizarContinuidadCommand command);
        Task<ContinuidadDto> ActualizarEstatusContinuidadAsync(EstatusContinuidadCommand command);
    }
}
