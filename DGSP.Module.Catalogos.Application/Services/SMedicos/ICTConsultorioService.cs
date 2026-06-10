using DGSP.Shared.Contracts.Commands.Catalogos.SMedicos.Consultorios;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;

namespace DGSP.Module.Catalogos.Application.Services.SMedicos
{
    public interface ICTConsultorioService
    {
        Task<List<CTConsultorioDto>> GetAllConsultoriosAsync();
        Task<CTConsultorioDto> GetConsulotorioByIdAsync(int id);
        Task<CTConsultorioDto> RegistrarConsultorioAsync(RegistrarCTConsultorioCommand command);
        Task<CTConsultorioDto> ActualizarConsultorioAsync(ActualizarCTConsultorioCommand command);
    }
}
