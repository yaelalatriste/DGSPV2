using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso
{
    public class ConcluirNotaTraspasoCommand : IRequest<NotaTraspasoDto>
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int EstatusId { get; set; }
        public bool Procesar { get; set; }
        public IFormFile? Entregable { get; set; }
        public string Observaciones { get; set; } = string.Empty;
    }
}
