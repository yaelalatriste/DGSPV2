namespace DGSP.Module.Catalogos.Domain.SMedicos
{
    public class CTTipoMovimiento
    {
        public int Id { get;set; }
        public string Nombre { get;set; } = string.Empty;
        public bool Entrada { get;set; }
        public bool Salida { get;set; }
        public bool Activo { get;set; }
    }
}
