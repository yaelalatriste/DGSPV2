using DGSP.Module.Catalogos.Application.Interfaces.SMedicos;
using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Module.Catalogos.Domain.SMedicos;
using DGSP.Module.Catalogos.Persistence.Repositories.SMedicos;
using DGSP.Shared.Contracts.Commands.Catalogos.SMedicos.Consultorios;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DGSP.Module.Catalogos.Persistence.Services.SMedicos
{
    public class CTConsultorioService : ICTConsultorioService
    {
        private readonly ICTConsultorioRepository _cTConsultorioRepository;

        public CTConsultorioService(ICTConsultorioRepository cTConsultorioRepository)
        {
            _cTConsultorioRepository = cTConsultorioRepository;
        }

        public async Task<List<CTConsultorioDto>> GetAllConsultoriosAsync()
        {
            var consultorios = await _cTConsultorioRepository.GetAllConsultoriosAsync();

            return consultorios.Select(c => new CTConsultorioDto {
                Id = c.Id,
                Clave = c.Clave,
                Nombre = c.Nombre,
                Tipo = c.Tipo,
                Poblacion = c.Poblacion,
                Extension = c.Extension,
                Activo = c.Activo,
                ExpedienteResponsable = c.ExpedienteResponsable
            }).ToList();
        }
        
        public async Task<CTConsultorioDto> GetConsulotorioByIdAsync(int id)
        {
            var consultorio = await _cTConsultorioRepository.GetConsulotorioByIdAsync(id);

            return new CTConsultorioDto
            {
                Id = consultorio.Id,
                Clave = consultorio.Clave,
                Nombre = consultorio.Nombre,
                Tipo = consultorio.Tipo,
                Poblacion = consultorio.Poblacion,
                Extension = consultorio.Extension,
                Activo = consultorio.Activo,
                ExpedienteResponsable = consultorio.ExpedienteResponsable
            };
        }

        public async Task<CTConsultorioDto> RegistrarConsultorioAsync(RegistrarCTConsultorioCommand command)
        {
            var consultorio = new CTConsultorio
            {
                Id = command.Id,
                Tipo = command.Tipo,
                Poblacion = command.Poblacion,
                Extension = command.Extension,
                ExpedienteResponsable = command.ExpedienteResponsable
            };

            await _cTConsultorioRepository.RegistrarConsultorioAsync(consultorio);
            await _cTConsultorioRepository.SaveChangesAsync();

            return new CTConsultorioDto
            {
                Id = consultorio.Id,
                Tipo = consultorio.Tipo,
                Poblacion = consultorio.Poblacion,
                Extension = consultorio.Extension,
                ExpedienteResponsable = command.ExpedienteResponsable
            };
        }

        public async Task<CTConsultorioDto> ActualizarConsultorioAsync(ActualizarCTConsultorioCommand command)
        {
            var consultorio = await _cTConsultorioRepository.GetConsulotorioByIdAsync(command.Id);

            consultorio.Tipo = command.Tipo;
            consultorio.Poblacion = command.Poblacion;
            consultorio.Extension = command.Extension;
            consultorio.ExpedienteResponsable = command.ExpedienteResponsable;

            await _cTConsultorioRepository.ActualizarConsultorioAsync(consultorio);

            return new CTConsultorioDto
            {
                Id = consultorio.Id,
                Clave = consultorio.Clave,
                Nombre = consultorio.Nombre,
                Tipo = consultorio.Tipo,
                Poblacion = consultorio.Poblacion,
                Extension = consultorio.Extension,
                Activo = consultorio.Activo,
                ExpedienteResponsable = consultorio.ExpedienteResponsable,
            };
        }
    }
}
