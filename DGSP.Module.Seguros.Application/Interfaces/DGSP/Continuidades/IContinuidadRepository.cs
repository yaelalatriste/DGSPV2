using DGSP.Module.Seguros.Domain.DGSP.Continuidades;

namespace DGSP.Module.Seguros.Application.Interfaces.DGSP.Continuidades
{
    public interface IContinuidadRepository
    {
        Task<List<Continuidad>> GetAllContinuidadesAsync();
        Task<List<Continuidad>> GetContinuidadesByAnio(int anio);        
        Task<List<Continuidad>> GetContinuidadesByEstatus(int estatus);        
        Task<Continuidad> GetContinuidadByIdAsync(int id);
        Task RegistrarContinuidadAsync(Continuidad command);
        Task ActualizarContinuidadAsync(Continuidad command);
        Task SaveChangesAsync();
    }
}
