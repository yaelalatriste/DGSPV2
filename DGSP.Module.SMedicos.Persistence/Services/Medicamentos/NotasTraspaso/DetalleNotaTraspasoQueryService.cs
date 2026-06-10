using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.NotasTraspaso;
using DGSP.Module.SMedicos.Application.Services.Medicamentos.NotasTraspaso;
using DGSP.Module.SMedicos.Domain.NotasTraspaso;
using DGSP.Module.SMedicos.Persistence.Repositories.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DGSP.Module.SMedicos.Persistence.Services.Medicamentos.NotasTraspaso
{
    public class DetalleNotaTraspasoQueryService : IDetalleNotaTraspasoQueryService
    {
        private readonly IDetalleNotaTraspasoRepository _detalleNotaTraspasoRepository;

        public DetalleNotaTraspasoQueryService(IDetalleNotaTraspasoRepository detalleNotaTraspasoRepository)
        {
            _detalleNotaTraspasoRepository = detalleNotaTraspasoRepository;
        }

        public async Task<DetalleNotaTraspasoDto> AddDetalleNotaTraspaso(RegistrarDetalleNotaTraspasoCommand command)
        {
            var detalle = new DetalleNotaTraspaso
            {
                UsuarioId = command.UsuarioId, 
                NotaId = command.NotaId,
                TipoMovimientoId = command.TipoMovimientoId,
                LoteId = command.LoteId,
                Almacen = command.Almacen,
                Cantidad = command.Cantidad,
                Restante = command.Restante,
                FechaCreacion = DateTime.Now,
            };

            await _detalleNotaTraspasoRepository.AddDetalleNotaTraspasoAsync(detalle);
            await _detalleNotaTraspasoRepository.SaveChangesAsync();

            return new DetalleNotaTraspasoDto
            {
                Id = detalle.Id,
                UsuarioId =command.UsuarioId,
                NotaId = command.NotaId,
                TipoMovimientoId = command.TipoMovimientoId,
                LoteId = command.LoteId,
                Almacen = command.Almacen,
                Cantidad = command.Cantidad,
                Restante = command.Restante,
                FechaCreacion = command.FechaCreacion,
            };
        }

        public async Task<DetalleNotaTraspasoDto> UpdateDetalleNotaTraspaso(ActualizarDetalleNotaTraspasoCommand command)
        {
            var detalle = await _detalleNotaTraspasoRepository.GetByIdAsync(command.Id);
            detalle.UsuarioId = command.UsuarioId;
            detalle.TipoMovimientoId = command.TipoMovimientoId;
            detalle.LoteId = command.LoteId;
            detalle.Almacen = command.Almacen;
            detalle.Cantidad = command.Cantidad;
            detalle.Restante = command.Restante;
            detalle.FechaActualizacion = DateTime.Now;

            await _detalleNotaTraspasoRepository.UpdateDetalleNotaTraspasoAsync(detalle);

            return new DetalleNotaTraspasoDto
            {
                Id = detalle.Id,
                UsuarioId = command.UsuarioId,
                NotaId = detalle.NotaId,
                TipoMovimientoId = command.TipoMovimientoId,
                LoteId = command.LoteId,
                Almacen = command.Almacen,
                Cantidad = command.Cantidad,
                Restante = command.Restante,
                FechaCreacion = detalle.FechaCreacion,
                FechaActualizacion = command.FechaActualizacion,
            };
        }

        public async Task<DetalleNotaTraspasoDto> DeleteDetalleNotaTraspaso(EliminarDetalleNotaTraspasoCommand command)
        {
            var detalle = await _detalleNotaTraspasoRepository.GetByIdAsync(command.Id);
            detalle.UsuarioId = command.UsuarioId;
            detalle.FechaEliminacion= DateTime.Now;

            await _detalleNotaTraspasoRepository.UpdateDetalleNotaTraspasoAsync(detalle);

            return new DetalleNotaTraspasoDto
            {
                Id = detalle.Id,
                UsuarioId = detalle.UsuarioId,
                NotaId = detalle.NotaId,
                TipoMovimientoId = detalle.TipoMovimientoId,
                LoteId = detalle.LoteId,
                Almacen = detalle.Almacen,
                Cantidad = detalle.Cantidad,
                Restante = detalle.Restante,
                FechaCreacion = detalle.FechaCreacion,
                FechaEliminacion = detalle.FechaEliminacion,
            };
        }

        public async Task EliminarDetalleNotaTraspaso(int id)
        {
            var detalle = await _detalleNotaTraspasoRepository.GetByIdAsync(id);
            _detalleNotaTraspasoRepository.Remove(detalle);
        }

        public async Task<DetalleNotaTraspasoDto> GetDetalleNotaTraspasoByIdAsync(int id)
        {
            var notas = await _detalleNotaTraspasoRepository.GetByIdAsync(id);

            return new DetalleNotaTraspasoDto
            {
                Id = notas.Id,
                NotaId = notas.NotaId,
                TipoMovimientoId = notas.TipoMovimientoId,
                LoteId = notas.LoteId,
                Almacen = notas.Almacen,
                Cantidad = notas.Cantidad,
                Restante = notas.Restante,
                FechaCreacion = notas.FechaCreacion,
            };
        }

        public async Task<List<DetalleNotaTraspasoDto>> GetDetallesNotaTraspasoByNotaAsync(int nota)
        {
            var notas = await _detalleNotaTraspasoRepository.GetDetallesByNotaIdAsync(nota);

            return notas.Select(n => new DetalleNotaTraspasoDto
            {
                Id = n.Id,
                NotaId = n.NotaId,
                TipoMovimientoId = n.TipoMovimientoId,
                LoteId = n.LoteId,
                Almacen = n.Almacen,
                Cantidad = n.Cantidad,
                Restante = n.Restante,
                FechaCreacion = n.FechaCreacion,
            }).ToList();
        }
    }
}
