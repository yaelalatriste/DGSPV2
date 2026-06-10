using Azure.Core;
using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.NotasTraspaso;
using DGSP.Module.SMedicos.Application.Services.Medicamentos.NotasTraspaso;
using DGSP.Module.SMedicos.Domain.NotasTraspaso;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using Microsoft.AspNetCore.Http;

namespace DGSP.Module.SMedicos.Persistence.Services.Medicamentos.NotasTraspaso
{
    public class NotaTraspasoQueryService : INotaTraspasoQueryService
    {
        private readonly INotaTraspasoRepository _notaTraspasoRepository;

        public NotaTraspasoQueryService(INotaTraspasoRepository notaTraspasoRepository)
        {
            _notaTraspasoRepository = notaTraspasoRepository;
        }

        public async Task<List<NotaTraspasoDto>> GetAllNotasTraspasoAsync()
        {
            var notas = await _notaTraspasoRepository.GetAllNotasTraspaso();

            return notas.Select(n => new NotaTraspasoDto
            {
                Id = n.Id,
                UsuarioId = n.UsuarioId,
                ConsultorioId = n.ConsultorioId,
                ConsultorioDestinoId = n.ConsultorioDestinoId,
                EstatusId = n.EstatusId,
                NumeroTraspaso = n.NumeroTraspaso,
                Entregable = n.Entregable,
                FechaCreacion = n.FechaCreacion
            }).ToList();
        }

        public async Task<NotaTraspasoDto> GetNotaTraspasoByIdAsync(int id)
        {
            var data = await _notaTraspasoRepository.GetNotaTraspasoById(id);

            var nota = new NotaTraspasoDto
            {
                Id = data.Id,
                UsuarioId = data.UsuarioId,
                ConsultorioId = data.ConsultorioId,
                ConsultorioDestinoId = data.ConsultorioDestinoId,
                EstatusId = data.EstatusId,
                NumeroTraspaso = data.NumeroTraspaso,
                Entregable = data.Entregable,
                FechaCreacion = data.FechaCreacion
            };

            return nota;
        }

        public async Task<NotaTraspasoDto> AddNotaTraspaso(RegistrarNotaTraspasoCommand command)
        {
            var notaTraspaso = new NotaTraspaso()
            {
                ConsultorioId = command.ConsultorioId,
                UsuarioId = command.UsuarioId,
                ConsultorioDestinoId = command.ConsultorioDestinoId,
                NumeroTraspaso = command.NumeroTraspaso,
                FechaCreacion = DateTime.Now
            };

            await _notaTraspasoRepository.AddNotaTraspasoAsync(notaTraspaso);
            await _notaTraspasoRepository.SaveChangesAsync();

            return new NotaTraspasoDto
            {
                Id = notaTraspaso.Id,
                UsuarioId = notaTraspaso.UsuarioId,
                ConsultorioId = notaTraspaso.ConsultorioId,
                ConsultorioDestinoId = notaTraspaso.ConsultorioDestinoId,
                NumeroTraspaso = notaTraspaso.NumeroTraspaso,
                Entregable = notaTraspaso.Entregable
            };
        }

        public async Task<NotaTraspasoDto> ActualizarNotaTraspaso(ActualizarNotaTraspasoCommand command)
        {
            var nota= await _notaTraspasoRepository.GetNotaTraspasoById(command.Id);
            nota.UsuarioId = command.UsuarioId;
            nota.EstatusId = command.EstatusId;
            nota.FechaActualizacion = DateTime.Now;

            await _notaTraspasoRepository.ActualizarNotaTraspasoAsync(nota);

            return new NotaTraspasoDto
            {
                Id = nota.Id,
                UsuarioId = nota.UsuarioId,
                ConsultorioId = nota.ConsultorioId,
                ConsultorioDestinoId = nota.ConsultorioDestinoId,
                EstatusId = nota.EstatusId,
                NumeroTraspaso = nota.NumeroTraspaso,
                Entregable = nota.Entregable,
                FechaActualizacion = nota.FechaActualizacion
            };
        }
        
        public async Task<NotaTraspasoDto> ActualizarEstatusNotaTraspaso(ConcluirNotaTraspasoCommand command)
        {
            var nota= await _notaTraspasoRepository.GetNotaTraspasoById(command.Id);
            nota.UsuarioId = command.UsuarioId;
            nota.EstatusId = command.EstatusId;
            nota.FechaActualizacion = DateTime.Now;

            await _notaTraspasoRepository.ActualizarNotaTraspasoAsync(nota);

            return new NotaTraspasoDto
            {
                Id = nota.Id,
                UsuarioId = nota.UsuarioId,
                ConsultorioId = nota.ConsultorioId,
                ConsultorioDestinoId = nota.ConsultorioDestinoId,
                EstatusId = nota.EstatusId,
                NumeroTraspaso = nota.NumeroTraspaso,
                Entregable = nota.Entregable,
                FechaActualizacion = nota.FechaActualizacion
            };
        }

        public async Task<NotaTraspasoDto> ConcluirNotaTraspaso(ConcluirNotaTraspasoCommand command)
        {
            var nota = await _notaTraspasoRepository.GetNotaTraspasoById(command.Id);
            DateTime fechaCreacion = DateTime.Now;
            string newDate = fechaCreacion.ToString("yyyyMMddHHmmss");
            string? archivo = null;

            if (command.Entregable != null)
            {
                if (await guardaArchivo(command.Entregable, nota.NumeroTraspaso, newDate))
                {
                    archivo = newDate + "_" + command.Entregable.FileName;
                }
            }

            nota.UsuarioId = command.UsuarioId;
            nota.EstatusId = command.EstatusId;
            nota.Entregable = archivo ?? "";
            nota.FechaActualizacion = fechaCreacion;
            
            await _notaTraspasoRepository.ActualizarNotaTraspasoAsync(nota);

            return new NotaTraspasoDto
            {
                Id = nota.Id,
                UsuarioId = nota.UsuarioId,
                ConsultorioId = nota.ConsultorioId,
                ConsultorioDestinoId = nota.ConsultorioDestinoId,
                EstatusId = nota.EstatusId,
                NumeroTraspaso = nota.NumeroTraspaso,
                Entregable = nota.Entregable,
                FechaActualizacion = nota.FechaActualizacion
            };
        }

        private async Task<bool> guardaArchivo(IFormFile archivo, string nota, string fecha)
        {
            long size = archivo.Length;

            string newPath = Directory.GetCurrentDirectory() + "\\Entregables\\SMedicos\\NotasTraspaso\\Memorandum\\" + nota;

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
    }
}
