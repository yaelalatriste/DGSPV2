namespace DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.SGMM
{
    public class ServidorPublicoOpMMS
    {
        public int fiExpSP { get;set;  }
        public string fcCvePuestoCJF { get;set;  } = string.Empty;
        public string fcCveNivelCJF { get;set;  } = string.Empty;
        public string fcCveAdscCJF { get;set;  } = string.Empty;
        public short fiIdSBasPueNiv { get;set;  } 
        public DateTime fdFchAntigGMM  { get;set;  }
        public DateTime fdFchAltaReg { get;set;  }
        public int fiUsrAltaReg { get;set;  }
        public DateTime fdFchModReg { get;set;  }
        public int fiUsrModReg { get;set;  }
        public bool flStatusSP { get;set;  }
        public int fiIdRegPueNiv { get;set;  }
    }
}
