namespace DGSP.Modules.SMedicos.Contract.DTOs.Siacom.Catalogos
{
    public class CTTipoConsultaDto
    {
        public int FiIdTipoConsulta { get; set; }

        public string FcTipoConsulta { get; set; } = null!;

        public short FiEspecialidad { get; set; }

        public bool FlEstatus { get; set; }

        public int FiExpAlta { get; set; }

        public DateTime FdFchAlta { get; set; }

        public Nullable<int> FiExpAct { get; set; }

        public Nullable<DateTime> FdFchAct { get; set; }
    }
}
