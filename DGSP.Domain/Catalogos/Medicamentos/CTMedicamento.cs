namespace DGSP.Domain.Catalogos.Medicamentos
{
    public class CTMedicamento
    {
        public int Id { get; set; }
        public string? Clave { get; set; }
        public string Nombre { get; set; } = default!;
        public bool Activo { get; set; } = true;
    }
}
