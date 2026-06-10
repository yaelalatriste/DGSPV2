using DGSP.Module.Seguros.Application.Interfaces.DGSP.Continuidades;
using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.OficiosContinuidades;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;

namespace DGSP.Module.Seguros.Persistence.Services.DGSP.Continuidades
{
    public class OficioContinuidadService : IOficioContinuidadService
    {
        private readonly IOficioContinuidadRepository _oficioContinuidadRepository;

        public OficioContinuidadService(IOficioContinuidadRepository oficioContinuidadRepository)
        {
            _oficioContinuidadRepository = oficioContinuidadRepository;
        }

        public async Task<List<OficioContinuidadDto>> GetOficiosByContinuidadAsync(int continuidadId)
        {
            var oficios = await _oficioContinuidadRepository.GetoficiosByContinuidadAsync(continuidadId);

            return oficios.Select(x => new OficioContinuidadDto
            {
                ContinuidadId = x.ContinuidadId,
                AnioMovimiento = x.AnioMovimiento,
                TipoMovimiento = x.TipoMovimiento,
                Expediente = x.Expediente,
                RegistroMovimiento = x.RegistroMovimiento,
                Oficio = x.Oficio,
                ObservacionMovimiento = x.ObservacionMovimiento,
                Validado = x.Validado,
                FechaAplicacionMovimientoSP = x.FechaAplicacionMovimientoSP,
                FechaAltaMovimiento = x.FechaAltaMovimiento,
                FechaCreacion = x.FechaCreacion,
                FechaActualizacion = x.FechaActualizacion
            }).ToList();
        }

        public async Task<OficioContinuidadDto> RegistrarOficioContinuidadService(RegistrarOficioContinuidadCommand command)
        {
            var oficio = new OficioContinuidad 
            {
                ContinuidadId = command.ContinuidadId,
                AnioMovimiento = command.AnioMovimiento,
                TipoMovimiento = command.TipoMovimiento,
                Expediente = command.Expediente,
                RegistroMovimiento = command.RegistroMovimiento,
                Oficio = command.Oficio,
                ObservacionMovimiento = command.ObservacionMovimiento,
                Validado = command.Validado,
                FechaAplicacionMovimientoSP = Convert.ToDateTime(command.FechaAplicacionMovimientoSP),
                FechaAltaMovimiento = Convert.ToDateTime(command.FechaAltaMovimiento),
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now,
            };

            await _oficioContinuidadRepository.RegistrarOficioContinuidadAsync(oficio);
            await _oficioContinuidadRepository.SaveChangesAsync();

            return new OficioContinuidadDto 
            {
                ContinuidadId = oficio.ContinuidadId,
                AnioMovimiento = oficio.AnioMovimiento,
                TipoMovimiento = oficio.TipoMovimiento,
                Expediente = oficio.Expediente,
                RegistroMovimiento = oficio.RegistroMovimiento,
                Oficio = oficio.Oficio,
                ObservacionMovimiento = oficio.ObservacionMovimiento,
                Validado = oficio.Validado,
                FechaAplicacionMovimientoSP = oficio.FechaAplicacionMovimientoSP,
                FechaAltaMovimiento = oficio.FechaAltaMovimiento,
                FechaCreacion = oficio.FechaCreacion
            };
        }
    }
}
