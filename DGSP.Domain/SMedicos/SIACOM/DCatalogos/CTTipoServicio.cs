namespace DGSP.Domain.SMedicos.SIACOM.DCatalogos
{
    public class CTTipoServicio
    {
        public int FiIdTipoServicio { get; set; }
        public string FcTipoServicio { get; set; } = null!;

        public bool FlEstatus { get; set; }

        public int FiExpAlta { get; set; }

        public DateTime FdFchAlta { get; set; }

        public int? FiExpAct { get; set; }

        public DateTime? FdFchAct { get; set; }
    }
}
