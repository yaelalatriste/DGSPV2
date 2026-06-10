namespace DGSP.Modules.SMedicos.Contract.DTOs.Siacom.Catalogos
{
    public class CTTipoConsultaDetalleDto
    {
        public int FiIdTipoConsultaDetalle { get; set; }

        public int FiIdTipoConsulta { get; set; }

        public string FcTipoConsultaDetalle { get; set; } = null!;

        public bool FlEstatus { get; set; }

        public int FiExpAlta { get; set; }

        public DateTime FdFchAlta { get; set; }

        public Nullable<int> FiExpAct { get; set; }

        public Nullable<DateTime> FdFchAct { get; set; }
    }
}
