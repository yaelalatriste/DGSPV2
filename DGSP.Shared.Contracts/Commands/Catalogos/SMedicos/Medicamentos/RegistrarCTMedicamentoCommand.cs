using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.Catalogos.SMedicos.Medicamentos
{
    public class RegistrarCTMedicamentoCommand : IRequest<CTMedicamentoDto>
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int TipoInsumoId { get; set; }
        public string Formula { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Presentacion { get; set; } = string.Empty;
        public int TipoEnvaseId { get; set; }
    }
}
