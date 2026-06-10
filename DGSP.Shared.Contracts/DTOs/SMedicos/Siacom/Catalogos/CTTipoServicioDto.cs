using System;

namespace DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Catalogos
{
    public class CTTipoServicioDto
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
