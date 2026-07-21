using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.OficiosContinuidades;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;

namespace DGSP.Module.DGRH.Application.Services.Seguros.Movimientos
{
    public interface IMovimientoSpRepository
    {
        Task<List<RegistrarOficioContinuidadCommand>> ObtenerMovimientoBajaAsync(ContinuidadDto continuidad);
    }
}
