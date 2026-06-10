using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.CEntregables;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;

namespace DGSP.Module.Seguros.Application.Services.DGSP.Continuidades
{
    public interface IEntregableContinuidadService
    {
        Task<List<EntregableContinuidadDto>> GetEntregablesByContinuidad(int continuidadId);
        Task<EntregableContinuidadDto> GetEntregableById(int entregableId);
        Task<EntregableContinuidadDto> RegistrarEntregableContinuidadAsync(RegistrarEntregableContinuidadCommand command);
        Task<EntregableContinuidadDto> ActualizarEntregableContinuidadAsync(ActualizarEntregableContinuidadCommand command);
        Task<EntregableContinuidadDto> EliminarEntregableContinuidadAsync(EliminarEntregableContinuidadCommand command);
    }
}
