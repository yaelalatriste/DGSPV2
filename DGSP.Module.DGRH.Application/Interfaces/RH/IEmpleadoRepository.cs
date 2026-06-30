using DGSP.Module.DGRH.Domain.RH.DEmpleado;

namespace DGSP.Module.DGRH.Application.Services.RH
{
    public interface IEmpleadoRepository
    {
        Task<List<Empleado>> GetAllEmpleados();
        Task<Empleado> GetEmpleado(int exp);
        Task<List<EmpleadoPuesto>> GetMovimientosEmpleado(int exp);
        Task<List<EmpleadoPuesto>> GetEmpleadosTEAsync();
    }
}
