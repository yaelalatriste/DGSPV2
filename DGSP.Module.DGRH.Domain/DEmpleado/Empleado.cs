namespace DGSP.Module.DGRH.Domain
{
    public class Empleado
    {
        public int exp { get; set; }

        public string inicial_rfc { get; set; } = null!;

        public DateTime fech_nacimiento { get; set; }

        public string? homonimia { get; set; }

        public string paterno { get; set; } = null!;

        public string? materno { get; set; }

        public string nombre { get; set; } = null!;

        public string? nss { get; set; }

        public string ind_forma_pago { get; set; } = null!;

        public string num_cuenta { get; set; } = null!;

        public decimal segapad { get; set; }

        public decimal segapad_ant { get; set; }

        public string ind_sexo { get; set; } = null!;

        public string ind_nacionalidad { get; set; } = null!;

        public decimal dias_trab_anio { get; set; }

        public decimal antig_pjf { get; set; }

        public decimal antig_gob { get; set; }

        public string? ced_profesional { get; set; }

        public string cuenta_sar { get; set; } = null!;

        public string ind_tpo_vivienda { get; set; } = null!;

        public string ind_edo_civil { get; set; } = null!;

        public short num_hijos { get; set; }

        public string? telefono { get; set; }

        public string? calle_numero { get; set; }

        public string? colonia { get; set; }

        public string? cod_postal { get; set; }

        public string? deleg_mpio { get; set; }

        public DateTime? fech_baja { get; set; }

        public string ind_empleado { get; set; } = null!;

        public short num_quinquenio { get; set; }

        public string ind_fdo_ahorro { get; set; } = null!;

        public DateTime? hra_entrada { get; set; }

        public string ind_pfovissste { get; set; } = null!;

        public decimal pje_pfovissste { get; set; }

        public DateTime? fech_credencial { get; set; }

        public int? num_credencial { get; set; }

        public int num_plaza { get; set; }

        public int num_plaza_ant { get; set; }

        public short cve_profesion { get; set; }

        public string ind_fideijm { get; set; } = null!;

        public short? ubic_fisica { get; set; }

        public string? cve_edo_lr { get; set; }

        public string? cve_cd_lr { get; set; }

        public string? lug_nacimiento { get; set; }

        public string? cve_edo_ln { get; set; }

        public DateTime? fech_prof { get; set; }

        public short? cambio { get; set; }

        public string? cve_agrupador { get; set; }

        public DateTime? fech_antig { get; set; }

        public DateTime? fech_alta { get; set; }

        public string ind_madre { get; set; } = null!;

        public string? tel_ofna { get; set; }

        public string? tel_fax { get; set; }

        public short? ind_smgm { get; set; }

        public string? curp { get; set; }

        public short? csc_nomb { get; set; }

        public short? segsei { get; set; }

        public string? rfc { get; set; }
    }
}