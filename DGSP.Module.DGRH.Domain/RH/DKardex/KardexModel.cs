namespace DGSP.Module.DGRH.Domain.RH.DKardex
{
    public class KardexModel
    {
        public int exp { get; set; }

        public short csc_nomb { get; set; }

        public string ind_empleado { get; set; } = null!;

        public DateTime fech_ini_nomb { get; set; }

        public DateTime fech_fin_nomb { get; set; }

        public string cve_adscripcion { get; set; } = null!;

        public string cve_puesto { get; set; } = null!;

        public short csc_puesto { get; set; }

        public string ind_rango { get; set; } = null!;

        public string ind_tpo_nomb { get; set; } = null!;

        public string? ind_baja { get; set; }

        public DateTime? fech_cierre { get; set; }

        public int num_plaza { get; set; }

        public DateTime fech_captura { get; set; }

        public int capturista { get; set; }

        public DateTime? fech_baja { get; set; }

        public string? ced_pleno { get; set; }

        public string? ind_nomb { get; set; }

        public string? ind_baj { get; set; }

        public string? observ { get; set; }

        public short? fiIdEstatusMov { get; set; }

        public int? id_sim { get; set; }

        public string? cve_adsc_com { get; set; }

        public string? ind_ponencia { get; set; }

        public string? pto_funcional { get; set; }

        public bool fbEstado { get; set; }
    }
}