using DGSP.Module.Estatus.Application.Interfaces.Continuidades;
using DGSP.Module.Estatus.Application.Services.Continuidades;
using DGSP.Shared.Contracts.DTOs.Estatus.Continuidades;

namespace DGSP.Module.Estatus.Persistence.Services.Continuidades
{
    public class EstatusContinuidadesService : IEstatusContinuidadesService
    {
        private readonly IEstatusContinuidadesRepository _estatusContinuidadesRepository;

        public EstatusContinuidadesService(IEstatusContinuidadesRepository estatusContinuidadesRepository)
        {
            _estatusContinuidadesRepository = estatusContinuidadesRepository;
        }

        public async Task<List<EstatusContinuidadDto>> GetAllEstatus()
        {
            var estatus = await _estatusContinuidadesRepository.GetAllEstatus();

            return estatus.Select(e => new EstatusContinuidadDto
            {
                Id = e.Id,
                Abreviacion = e.Abreviacion,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,
                Icono = e.Icono,
                Fondo = e.Fondo,
                FondoHexadecimal = e.FondoHexadecimal,
                Orden = e.Orden,
                FechaCreacion = e.FechaCreacion,
            }).ToList();

        }

        public async Task<EstatusContinuidadDto> GetEstatusById(int id)
        {
            var estatus = await _estatusContinuidadesRepository.GetEstatusById(id);

            return new EstatusContinuidadDto
            {
                Id = estatus.Id,
                Abreviacion = estatus.Abreviacion,
                Nombre = estatus.Nombre,
                Descripcion = estatus.Descripcion,
                Icono = estatus.Icono,
                Fondo = estatus.Fondo,
                FondoHexadecimal = estatus.FondoHexadecimal,
                Orden = estatus.Orden,
                FechaCreacion = estatus.FechaCreacion,
            };
        }
    }
}
