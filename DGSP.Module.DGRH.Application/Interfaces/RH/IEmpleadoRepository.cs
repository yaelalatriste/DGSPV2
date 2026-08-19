using DGSP.Module.DGRH.Domain.RH.DEmpleado;
using DGSP.Module.DGRH.Domain.RH.DPuestos;
using DGSP.Shared.Contracts.DTOs.DGRH.RH.Empleados;

namespace DGSP.Module.DGRH.Application.Services.RH
{
    public interface IEmpleadoRepository
    {
        Task<List<Empleado>> GetAllEmpleados();
        Task<Empleado> GetEmpleado(int exp);
        Task<List<EmpleadoPuesto>> GetMovimientosEmpleado(int exp);
        Task<List<EmpleadoPuesto>> GetEmpleadosTEAsync();
        Task<List<UltimoPuestoEmpleadoDto>> GetUltimosPuestosAsync(IReadOnlyCollection<int> expedientes, CancellationToken cancellationToken = default);
    }
}
