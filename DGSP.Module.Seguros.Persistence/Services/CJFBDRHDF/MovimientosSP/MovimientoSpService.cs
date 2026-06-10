using DGSP.Module.Seguros.Application.Services.CJFBDRHDF.SGMM;
using DGSP.Module.Seguros.Domain.CJFBDRHDF;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.OficiosContinuidades;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Seguros.Persistence.Services.CJFBDRHDF.MovimientosSP
{
    public class MovimientoSpService : IMovimientoSpService
    {
        private readonly SegurosSGMMContext _context;

        public MovimientoSpService(SegurosSGMMContext context)
        {
            _context = context;
        }

        public async Task<List<RegistrarOficioContinuidadCommand>> ObtenerMovimientoBajaAsync(ContinuidadDto continuidad)
        {
            var oficio = await _context.MovimientosSP
                .Where(mov =>
                    (mov.fiIdMov == Convert.ToInt32(TipoMovimientoSp.Baja) || mov.fiIdMov == Convert.ToInt32(TipoMovimientoSp.BajaLineamientos)) &&
                    mov.fiExpSp == continuidad.Expediente &&
                    mov.fdFchAplicMovSp.Date == continuidad.FechaBaja.GetValueOrDefault().Date
                )
                .Select(mov => new
                {
                    mov,
                    cor = _context.Correspondencias
                        .Where(c =>
                            c.fiIdRegOfic == mov.fiIdRegOfic && c.flValidado && !EF.Functions.Like(c.fcNumSalida, "%[^0-9]%"))
                        .Select(c => new { c.fcNumSalida, c.flValidado })
                        .FirstOrDefault()
                })
                .Select(x => new RegistrarOficioContinuidadCommand
                {
                    ContinuidadId = continuidad.Id,
                    AnioMovimiento = x.mov.fiAnioMovSp,
                    Expediente = x.mov.fiExpSp,
                    TipoMovimiento = x.mov.fiIdMov,
                    RegistroMovimiento = x.mov.fiUsrAltaMovSp,
                    Oficio = x.cor != null ? Convert.ToInt32(x.cor.fcNumSalida) : 0,
                    ObservacionMovimiento = string.Empty,
                    Validado = x.cor != null && x.cor.flValidado,
                    FechaAplicacionMovimientoSP = x.mov.fdFchAplicMovSp,
                    FechaAltaMovimiento = x.mov.fdFchAltaMovSp,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                })
                .ToListAsync();

            return oficio;
        }
    }
}
