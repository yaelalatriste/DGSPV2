using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.OficiosContinuidades;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;

namespace DGSP.Module.Seguros.Application.Services.CJFBDRHDF.SGMM
{
    public interface IMovimientoSpService
    {
        Task<List<RegistrarOficioContinuidadCommand>> ObtenerMovimientoBajaAsync(ContinuidadDto continuidad);
    }
}
