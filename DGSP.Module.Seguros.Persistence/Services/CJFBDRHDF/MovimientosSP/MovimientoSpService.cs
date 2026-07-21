using DGSP.Module.DGRH.Application.Services.Seguros.Movimientos;
using DGSP.Module.Seguros.Application.Services.CJFBDRHDF.SGMM;
using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.OficiosContinuidades;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;

namespace DGSP.Module.DGRH.Persistence.Services.RH.Empleados
{
    public class MovimientoSpService : IMovimientoSpService
    {
        private readonly IMovimientoSpRepository _movimientoSpRepository;

        public MovimientoSpService(IMovimientoSpRepository movimientoSpRepository)
        {
            _movimientoSpRepository = movimientoSpRepository;
        }

        public async Task<List<RegistrarOficioContinuidadCommand>> ObtenerMovimientoBajaAsync(ContinuidadDto continuidad)
        {
            var oficio = await _movimientoSpRepository.ObtenerMovimientoBajaAsync(continuidad);

            return oficio.Select(x => new RegistrarOficioContinuidadCommand
            {
                ContinuidadId = continuidad.Id,
                AnioMovimiento = x.AnioMovimiento,
                Expediente = x.Expediente,
                TipoMovimiento = x.TipoMovimiento,
                RegistroMovimiento = x.RegistroMovimiento,
                Oficio = x.Oficio != 0 ? Convert.ToInt32(x.Oficio) : 0,
                ObservacionMovimiento = string.Empty,
                Validado = x.Validado != null && x.Validado,
                FechaAplicacionMovimientoSP = x.FechaAplicacionMovimientoSP,
                FechaAltaMovimiento = x.FechaAltaMovimiento,
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            }).ToList();
        }
    }
}
