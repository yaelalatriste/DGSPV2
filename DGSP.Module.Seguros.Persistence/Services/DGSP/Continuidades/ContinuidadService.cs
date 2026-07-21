using DGSP.Module.Seguros.Application.Interfaces.DGSP.Continuidades;
using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.Continuidad;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;

namespace DGSP.Module.Seguros.Persistence.Services.DGSP.Continuidades
{
    public class ContinuidadService : IContinuidadService
    {
        private readonly IContinuidadRepository _continuidadRepository;

        public ContinuidadService(IContinuidadRepository continuidadRepository)
        {
            _continuidadRepository = continuidadRepository;
        }

        public async Task<List<ContinuidadDto>> GetAllContinuidadesAsync()
        {
            var continuidades = await _continuidadRepository.GetAllContinuidadesAsync();

            return continuidades.Select(c => new ContinuidadDto {
                Id = c.Id,
                UsuarioId = c.UsuarioId,
                EstatusId = c.EstatusId,
                Expediente = c.Expediente,
                FechaBaja = c.FechaBaja,
                FechaEnvioSp = c.FechaEnvioSp,
                FechaLimitePago = c.FechaLimitePago,
                Importe = c.Importe,
                Pagado = c.Pagado,
                FechaCreacion = c.FechaCreacion,
                FechaActualizacion = c.FechaActualizacion,
                FechaEliminacion = c.FechaEliminacion,
            }).ToList();
        }

        public async Task<ContinuidadDto> GetContinuidadByIdAsync(int id)
        {
            var continuidad = await _continuidadRepository.GetContinuidadByIdAsync(id);

            return new ContinuidadDto
            {
                Id = continuidad.Id,
                UsuarioId = continuidad.UsuarioId,
                EstatusId = continuidad.EstatusId,
                Expediente = continuidad.Expediente,
                FechaBaja = continuidad.FechaBaja,
                FechaEnvioSp = continuidad.FechaEnvioSp,
                FechaLimitePago = continuidad.FechaLimitePago,
                Importe = continuidad.Importe,
                Pagado = continuidad.Pagado,
                FechaCreacion = continuidad.FechaCreacion,
                FechaActualizacion = continuidad.FechaActualizacion,
            };
        }

        public async Task<List<ContinuidadDto>> GetContinuidadesByAnio(int anio)
        {
            var continuidades = await _continuidadRepository.GetContinuidadesByAnio(anio);

            return continuidades.Select(c => new ContinuidadDto
            {
                Id = c.Id,
                UsuarioId = c.UsuarioId,
                EstatusId = c.EstatusId,
                Expediente = c.Expediente,
                FechaBaja = c.FechaBaja,
                FechaEnvioSp = c.FechaEnvioSp,
                FechaLimitePago = c.FechaLimitePago,
                Importe = c.Importe,
                Pagado = c.Pagado,
                FechaCreacion = c.FechaCreacion,
                FechaActualizacion = c.FechaActualizacion,
                FechaEliminacion = c.FechaEliminacion,
            }).ToList();
        }
        
        public async Task<List<ContinuidadDto>> GetContinuidadesByEstatus(int estatus)
        {
            var continuidades = await _continuidadRepository.GetContinuidadesByEstatus(estatus);

            return continuidades.Select(c => new ContinuidadDto
            {
                Id = c.Id,
                UsuarioId = c.UsuarioId,
                EstatusId = c.EstatusId,
                Expediente = c.Expediente,
                FechaBaja = c.FechaBaja,
                FechaEnvioSp = c.FechaEnvioSp,
                FechaLimitePago = c.FechaLimitePago,
                Importe = c.Importe,
                Pagado = c.Pagado,
                FechaCreacion = c.FechaCreacion,
                FechaActualizacion = c.FechaActualizacion,
                FechaEliminacion = c.FechaEliminacion,
            }).ToList();
        }

        public async Task<ContinuidadDto> RegistrarContinuidadAsync(RegistrarContinuidadCommand command)
        {
            var continuidad = new Continuidad 
            {
                UsuarioId = command.UsuarioId,
                Expediente = command.Expediente,
                FechaCreacion = DateTime.Now,
            };

            await _continuidadRepository.RegistrarContinuidadAsync(continuidad);
            await _continuidadRepository.SaveChangesAsync();

            return new ContinuidadDto
            {
                Id = continuidad.Id,
                UsuarioId = command.UsuarioId,
                EstatusId = continuidad.EstatusId,
                Expediente = command.Expediente,
                FechaBaja = command.FechaBaja,
                FechaEnvioSp = continuidad.FechaEnvioSp,
                FechaLimitePago = continuidad.FechaLimitePago,
                Importe = continuidad.Importe,
                Pagado = continuidad.Pagado,
                FechaCreacion = continuidad.FechaCreacion,
                FechaActualizacion = continuidad.FechaActualizacion,
            };
        }

        public async Task<ContinuidadDto> ActualizarContinuidadAsync(ActualizarContinuidadCommand command)
        {
            var continuidad = await _continuidadRepository.GetContinuidadByIdAsync(command.Id);

            continuidad.UsuarioId = command.UsuarioId;
            continuidad.FechaBaja = (command.FechaBaja.Year == 1 ? continuidad.FechaBaja : command.FechaBaja);
            continuidad.FechaEnvioSp = (command.FechaEnvioSP.Year == 1 ? continuidad.FechaEnvioSp: command.FechaEnvioSP);
            continuidad.FechaLimitePago = (command.FechaLimitePago.Year == 1 ? continuidad.FechaLimitePago : command.FechaLimitePago);
            continuidad.Importe = (command.Importe == 0 ? continuidad.Importe : command.Importe);
            continuidad.FechaActualizacion = DateTime.Now;

            await _continuidadRepository.ActualizarContinuidadAsync(continuidad);

            return new ContinuidadDto
            {
                Id = command.Id,
                UsuarioId = command.UsuarioId,
                EstatusId = continuidad.EstatusId,
                Expediente = continuidad.Expediente,
                FechaBaja = command.FechaBaja,
                FechaEnvioSp = continuidad.FechaEnvioSp,
                FechaLimitePago = continuidad.FechaLimitePago,
                Importe = continuidad.Importe,
                Pagado = continuidad.Pagado,
                FechaCreacion = continuidad.FechaCreacion,
                FechaActualizacion = continuidad.FechaActualizacion,
            };
        }

        public async Task<ContinuidadDto> ActualizarEstatusContinuidadAsync(EstatusContinuidadCommand command)
        {
            var continuidad = await _continuidadRepository.GetContinuidadByIdAsync(command.Id);

            continuidad.UsuarioId = command.UsuarioId;
            continuidad.EstatusId= command.EstatusId;
            continuidad.FechaActualizacion = DateTime.Now;

            await _continuidadRepository.ActualizarContinuidadAsync(continuidad);

            return new ContinuidadDto
            {
                Id = command.Id,
                UsuarioId = command.UsuarioId,
                EstatusId = command.EstatusId,
                Expediente = continuidad.Expediente,
                FechaBaja = continuidad.FechaBaja,
                FechaEnvioSp = continuidad.FechaEnvioSp,
                FechaLimitePago = continuidad.FechaLimitePago,
                Importe = continuidad.Importe,
                Pagado = continuidad.Pagado,
                FechaCreacion = continuidad.FechaCreacion,
                FechaActualizacion = continuidad.FechaActualizacion,
            };
        }
    }
}
