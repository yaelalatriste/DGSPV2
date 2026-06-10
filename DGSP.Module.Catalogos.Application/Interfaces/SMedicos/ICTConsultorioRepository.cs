using DGSP.Module.Catalogos.Domain.SMedicos;

namespace DGSP.Module.Catalogos.Application.Interfaces.SMedicos
{
    public interface ICTConsultorioRepository
    {
        Task<List<CTConsultorio>> GetAllConsultoriosAsync();
        Task<CTConsultorio> GetConsulotorioByIdAsync(int id);
        Task RegistrarConsultorioAsync(CTConsultorio Consultorio);
        Task ActualizarConsultorioAsync(CTConsultorio Consultorio);
        Task SaveChangesAsync();
    }
}
