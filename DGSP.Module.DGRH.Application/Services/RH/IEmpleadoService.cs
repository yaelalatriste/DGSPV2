using DGSP.Shared.Contracts.DTOs.DGRH.RH.Empleados;

namespace DGSP.Module.DGRH.Application.Services.RH
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoDto>> GetAllEmpleados();
        Task<EmpleadoDto> GetEmpleado(int exp);
        Task<List<EmpleadoDto>> GetMovimientosEmpleado(int exp);
        Task<List<EmpleadoDto>> GetEmpleadosTEAsync();
    }
}
