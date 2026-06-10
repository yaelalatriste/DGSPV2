using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using DGSP.Shared.Contracts.DTOs.Estatus.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Logs;
using DGSP.Shared.Contracts.DTOs.Usuarios;

namespace DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso
{
    public class NotaTraspasoDto
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int ConsultorioId { get; set; }
        public int ConsultorioDestinoId { get; set; }
        public int EstatusId { get; set; }
        public string NumeroTraspaso { get; set; } = string.Empty;
        public string Entregable { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public Nullable<DateTime> FechaActualizacion { get; set; }

        public CTConsultorioDto ConsultorioOrigen {  get; set; } = new CTConsultorioDto();
        public CTConsultorioDto ConsultorioDestino {  get; set; } = new CTConsultorioDto();
        public ENotaTraspasoDto Estatus{  get; set; } = new ENotaTraspasoDto();
        public UsuarioDto Usuario {  get; set; } = new UsuarioDto();
        public List<LogNotaTraspasoDto> Logs {  get; set; } = new List<LogNotaTraspasoDto>();
    }
}
