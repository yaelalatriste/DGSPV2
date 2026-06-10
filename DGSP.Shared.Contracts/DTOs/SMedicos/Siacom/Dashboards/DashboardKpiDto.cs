using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Dashboards
{
    public class DashboardKpiDto
    {
        public int TotalConsultas { get; set; }
        public string TipoConsultaMasFrecuente { get; set; } = string.Empty;
        public string MesMayorDemanda { get; set; } = string.Empty;
        public decimal VariacionPorcentual { get; set; }
    }
}
