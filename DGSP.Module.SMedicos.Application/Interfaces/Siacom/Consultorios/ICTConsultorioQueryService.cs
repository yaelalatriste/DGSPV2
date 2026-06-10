using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Catalogos;

namespace DGSP.Module.SMedicos.Application.Interfaces.Siacom.Consultorios
{
    public interface ICTConsultorioQueryService
    {
        Task<List<CTConsultorioSiacomDto>> GetAllConsultorios();
        Task<CTConsultorioSiacomDto> GetConsultorioById(int id);
    }
}
