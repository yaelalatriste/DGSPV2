namespace DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF
{
    public class CorrespondenciaDto
    {
        /// <summary>
        /// Identificador del registro propio de la tabla
        /// </summary>
        public int FiIdRegOfic { get; set; }

        /// <summary>
        /// Año del registro de correspondencia
        /// </summary>
        public short FiAnioRegOfic { get; set; }

        /// <summary>
        /// No. de folio del remitente de la solicitud del movimiento en póliza
        /// </summary>
        public string? FcFolOficRemit { get; set; }

        /// <summary>
        /// Fecha del oficio del remitente de la solicitud del movimiento en póliza
        /// </summary>
        public DateTime? FdFchOficRemit { get; set; }

        /// <summary>
        /// No. de folio asignado en la oficialia
        /// </summary>
        public string? FcFolioOficialia { get; set; }

        /// <summary>
        /// Fecha en que se recibe en oficialía la solicitud del movimiento en póliza
        /// </summary>
        public DateTime? FdFchOficialia { get; set; }

        /// <summary>
        /// Número de registro de salida asociada a los moviientos de póliza
        /// </summary>
        public string? FcNumSalida { get; set; }

        /// <summary>
        /// Indicador de si la solicitud de movimiento en póliza llegó a través del módulo de Metlife. 0 = No; 1 = Sí
        /// </summary>
        public bool? FlIndModulo { get; set; }

        /// <summary>
        /// Número asignado en el módulo, en caso de que la solicitud haya llegado por el módulo de Metlife
        /// </summary>
        public string? FcNumModulo { get; set; }

        /// <summary>
        /// Fecha en que se recibe la solicitud de movimiento en póliza en el módulo de Metlife
        /// </summary>
        public DateTime? FdFchModulo { get; set; }

        /// <summary>
        /// Fecha en que se da de alta el registro de correspondencia en la tabla
        /// </summary>
        public DateTime FdFchAlta { get; set; }

        /// <summary>
        /// Expediente del actor que da de alta el registro de la correspondencia en la póliza
        /// </summary>
        public int FiUsrAlta { get; set; }

        /// <summary>
        /// Fecha en que se realizan modificaciones al registro de correspondencia
        /// </summary>
        public DateTime? FdFchModif { get; set; }

        /// <summary>
        /// Expediente del actor que realiza modificaciones al registro de correspondencia
        /// </summary>
        public int? FiUsrModif { get; set; }

        /// <summary>
        /// Indica si el registro esta activo para usarse en el sistema
        /// </summary>
        public bool FlValidado { get; set; }

        /// <summary>
        /// Año en el que se envia la correspondencia a las aministraciones regionales
        /// </summary>
        public short? FiAnioEnvioFor { get; set; }

        /// <summary>
        /// Número del folio de ofilicalia de la aministración regional para control interno de los movimientos de la póliza
        /// </summary>
        public string? FcFolOficFor { get; set; }

        /// <summary>
        /// Fecha de validación de correspondencia
        /// </summary>
        public DateTime? FdFchValidacion { get; set; }

        /// <summary>
        /// Fecha en la que se efectuó el envío a las administraciones regionales
        /// </summary>
        public DateTime? FdFchEnvioFor { get; set; }

    }
}
