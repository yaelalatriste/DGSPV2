using System;

namespace DGSP.Gateway.Proxy.Services.Dashboards.Models
{
    internal class ContinuidadDashboardItem
    {
        public int Id { get; init; }

        public int Expediente { get; init; }

        public int EstatusId { get; init; }

        public DateTime FechaCreacion { get; init; }

        public bool Pagado { get; init; }

        public string Nivel { get; init; } = string.Empty;

        public string TipoPersonal { get; init; } = string.Empty;
    }
}
