using DGSP.Module.SMedicos.Domain.Inventarios;
using DGSP.Module.SMedicos.Domain.NotasTraspaso;

namespace DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.NotasTraspaso
{
    public interface IDetalleNotaTraspasoRepository
    {
        Task<List<DetalleNotaTraspaso>> GetDetallesByNotaIdAsync(int nota);
        Task<DetalleNotaTraspaso> GetByIdAsync(int id);
        Task AddDetalleNotaTraspasoAsync(DetalleNotaTraspaso entity);
        Task UpdateDetalleNotaTraspasoAsync(DetalleNotaTraspaso entity);
        //Task DeleteDetalleNotaTraspasoAsync(DetalleNotaTraspaso entity);
        void Remove(DetalleNotaTraspaso entity);
        Task SaveChangesAsync();
    }
}
