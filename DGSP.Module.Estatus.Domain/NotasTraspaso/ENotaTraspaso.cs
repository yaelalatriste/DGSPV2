namespace DGSP.Module.Estatus.Domain.NotasTraspaso
{
    public class ENotaTraspaso
    {
        public int Id { get; set; }
        public string Abreviacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Orden  { get; set; }
        public bool Editar { get; set; }
        public bool MemorandumPdf { get; set; }
        public bool MemorandumWord { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
