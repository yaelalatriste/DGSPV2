namespace DGSP.Domain.SMedicos.Medicamentos
{
    public class MedicamentoLote
    {
        public int LoteId { get; set; }
        public int ConsultorioId { get; set; }
        public int MedicamentoId { get; set; }
        public string Lote { get; set; } = default!;
        public DateTime FechaCaducidad { get; set; }
        public string FormaFarmaceutica { get; set; } = default!;
        public string TipoEnvase { get; set; } = default!;
        public int Cantidad { get; set; }
        public int CantidadEnvase { get; set; }
        public int CantidadTotal { get; set; }
        public string Concentracion { get; set; } = default!;
        public string UnidadContenido { get; set; } = default!;
        public string Notas { get; set; } = default!;
    }
}
