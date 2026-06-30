namespace DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Catalogos
{
    public class RangoEdadDto
    {
        public int Id { get; set; }
        public int LimiteInferior { get; set; }
        public int LimiteSuperior { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}
