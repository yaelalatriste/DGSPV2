using DGSP.Module.SMedicos.Domain.NotasTraspaso;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;

namespace DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.NotasTraspaso
{
    public interface INotaTraspasoRepository
    {
        Task<List<NotaTraspaso>> GetAllNotasTraspaso();
        Task<NotaTraspaso> GetNotaTraspasoById(int id);
        Task AddNotaTraspasoAsync(NotaTraspaso entity);
        Task ActualizarNotaTraspasoAsync(NotaTraspaso entity);
        Task SaveChangesAsync();
    }
}
