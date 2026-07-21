using DGSP.Module.Seguros.Application.Interfaces.DGSP.Continuidades;
using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.Entregables;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;
using Microsoft.AspNetCore.Http;

namespace DGSP.Module.Seguros.Persistence.Services.DGSP.Continuidades
{
    public class EntregableContinuidadService : IEntregableContinuidadService
    {
        private readonly IEntregableContinuidadRepository _entregableContinuidadRepository;

        public EntregableContinuidadService(IEntregableContinuidadRepository entregableContinuidadRepository)
        {
            _entregableContinuidadRepository = entregableContinuidadRepository;
        }

        public async Task<List<EntregableContinuidadDto>> GetEntregablesByContinuidad(int continuidadId)
        {
            var entregables = await _entregableContinuidadRepository.GetEntregablesByContinuidadAsync(continuidadId);

            return entregables.Select(e => new EntregableContinuidadDto {
                Id = e.Id,
                UsuarioId = e.UsuarioId,
                ContinuidadId = e.ContinuidadId,
                EntregableId = e.EntregableId,
                Archivo = e.Archivo,
                FechaCreacion = e.FechaCreacion,
                FechaActualizacion = e.FechaActualizacion,
                FechaEliminacion = e.FechaEliminacion,
            }).ToList();
        }
        
        public async Task<EntregableContinuidadDto> GetEntregableById(int id)
        {
            var entregable = await _entregableContinuidadRepository.GetEntregableByIdAsync(id);

            return new EntregableContinuidadDto {
                Id = entregable.Id,
                UsuarioId = entregable.UsuarioId,
                ContinuidadId = entregable.ContinuidadId,
                EntregableId = entregable.EntregableId,
                Archivo = entregable.Archivo,
                FechaCreacion = entregable.FechaCreacion,
                FechaActualizacion = entregable.FechaActualizacion,
            };
        }

        public async Task<EntregableContinuidadDto> RegistrarEntregableContinuidadAsync(RegistrarEntregableContinuidadCommand command)
        {
            DateTime fechaCreacion = DateTime.Now;
            string newDate = fechaCreacion.ToString("yyyyMMddHHmmss");
            string archivo = string.Empty;

            if (command.Entregable != null)
            {
                if (await guardaArchivo(command.Entregable, command.Expediente, command.TipoEntregable, newDate))
                {
                    archivo = newDate + "_" + command.Entregable.FileName;
                }
            }

            var entregable = new EntregableContinuidad 
            { 
                UsuarioId = command.UsuarioId,
                ContinuidadId = command.ContinuidadId,
                EntregableId = command.EntregableId,
                Archivo = archivo,
                FechaCreacion = fechaCreacion
            };

            await _entregableContinuidadRepository.RegistrarEntregableContinuidadAsync(entregable);
            await _entregableContinuidadRepository.SaveChangesAsync();

            return new EntregableContinuidadDto
            {
                Id = entregable.Id,
                UsuarioId = entregable.UsuarioId,
                ContinuidadId = entregable.ContinuidadId,
                EntregableId = entregable.EntregableId,
                Archivo = entregable.Archivo,
                FechaCreacion = entregable.FechaCreacion,
                FechaActualizacion = entregable.FechaActualizacion,
            };
        }

        public async Task<EntregableContinuidadDto> ActualizarEntregableContinuidadAsync(ActualizarEntregableContinuidadCommand command)
        {
            var entregable = await _entregableContinuidadRepository.GetEntregableByIdAsync(command.Id);
            DateTime fechaActualizacion = DateTime.Now;
            string newDate = fechaActualizacion.ToString("yyyyMMddHHmmss");
            string archivo = string.Empty;

            if (command.Entregable != null)
            {
                if (eliminaArchivo(command.Expediente, command.TipoEntregable, entregable.Archivo))
                {
                    if (await guardaArchivo(command.Entregable, command.Expediente, command.TipoEntregable, newDate))
                    {
                        archivo = newDate + "_" + command.Entregable.FileName;
                    }
                }
            }

            entregable.UsuarioId = command.UsuarioId;
            entregable.ContinuidadId = command.ContinuidadId;
            entregable.EntregableId = command.EntregableId;
            entregable.Archivo = (archivo.Equals("") ? entregable.Archivo : archivo);
            entregable.FechaActualizacion= fechaActualizacion;

            await _entregableContinuidadRepository.ActualizarEntregableContinuidadAsync(entregable);

            return new EntregableContinuidadDto
            {
                Id = entregable.Id,
                UsuarioId = entregable.UsuarioId,
                ContinuidadId = entregable.ContinuidadId,
                EntregableId = entregable.EntregableId,
                Archivo = entregable.Archivo,
                FechaCreacion = entregable.FechaCreacion,
                FechaActualizacion = entregable.FechaActualizacion,
            };
        }

        public async Task<EntregableContinuidadDto> EliminarEntregableContinuidadAsync(EliminarEntregableContinuidadCommand command)
        {
            var entregable = await _entregableContinuidadRepository.GetEntregableByIdAsync(command.Id);
            DateTime fechaEliminacion = DateTime.Now;

            if (eliminaArchivo(command.Expediente, command.TipoEntregable, entregable.Archivo))
            {
                entregable.UsuarioId = command.UsuarioId;
                entregable.FechaEliminacion = fechaEliminacion;
            }
            
            await _entregableContinuidadRepository.ActualizarEntregableContinuidadAsync(entregable);

            return new EntregableContinuidadDto
            {
                Id = entregable.Id,
                UsuarioId = entregable.UsuarioId,
                ContinuidadId = entregable.ContinuidadId,
                EntregableId = entregable.EntregableId,
                Archivo = entregable.Archivo,
                FechaCreacion = entregable.FechaCreacion,
                FechaActualizacion = entregable.FechaActualizacion,
                FechaEliminacion = entregable.FechaEliminacion,
            };
        }

        private async Task<bool> guardaArchivo(IFormFile archivo, int expediente, string tipoEntregable, string fecha)
        {
            string newPath = Directory.GetCurrentDirectory() + "\\Entregables\\Seguros\\Continuidades\\" + expediente + "\\" + tipoEntregable;

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            using (var stream = new FileStream(newPath + "\\" + fecha + "_" + archivo.FileName, FileMode.Create))
            {
                try
                {
                    await archivo.CopyToAsync(stream);
                    return true;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message.ToString();
                    return false;
                }
            }
        }

        private bool eliminaArchivo(int expediente, string tipoEntregable, string archivo)
        {
            string newPath = Directory.GetCurrentDirectory() + "\\Entregables\\Seguros\\Continuidades\\" + expediente + "\\" + tipoEntregable;

            var extensions = new List<string> { ".PDF", ".pdf", ".xlsx", ".XLSX", ".zip", ".ZIP" };

            var files = Directory.GetFiles(newPath, "*.*", SearchOption.AllDirectories)
                                .Where(f => extensions
                                .Any(extn => string.Compare(Path.GetExtension(f), extn, StringComparison.InvariantCultureIgnoreCase) == 0))
                                .ToArray();
            if (files.Count() != 0)
            {
                var exist = files.Where(f => f.Contains(archivo)).First();

                try
                {
                    File.Delete(exist);
                    return true;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message.ToString();
                    return false;
                }
            }
            return true;
        }
    }
}
