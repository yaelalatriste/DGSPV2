using DGSP.Gateway.Proxy.Queries.DGRH.Empleados;
using DGSP.Gateway.Proxy.Queries.Estatus.Continuidades;
using DGSP.Gateway.Proxy.Queries.Seguros.DGSP.Siniestros.Continuidades;
using DGSP.Gateway.Proxy.Services.Dashboards.Models;
using DGSP.Shared.Contracts.DTOs.Estatus.Continuidades;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Dashboard;
using DGSP.Shared.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Services.Dashboards
{
    public interface IDashboardContinuidadesService
    {
        Task<DashboardContinuidadesDto> ObtenerDashboardAsync(DashboardFiltroDto filtro);
    }

    public class DashboardContinuidadesService : IDashboardContinuidadesService
    {
        private readonly IQContinuidadProxy _continuidadProxy;
        private readonly IQEmpleadoProxy _empleadoProxy;
        private readonly IQEstatusContinuidadProxy _estatusProxy;

        public DashboardContinuidadesService(IQContinuidadProxy continuidadProxy,IQEmpleadoProxy empleadoProxy,
            IQEstatusContinuidadProxy estatusProxy)
        {
            _continuidadProxy = continuidadProxy;
            _empleadoProxy = empleadoProxy;
            _estatusProxy = estatusProxy;
        }

        public async Task<DashboardContinuidadesDto> ObtenerDashboardAsync(DashboardFiltroDto filtro)
        {
            ValidarFiltro(filtro);

            var fechaInicio = new DateTime(
                filtro.Anio,
                filtro.Mes,
                1);

            var fechaFin = fechaInicio.AddMonths(1);

            /*
             * Las continuidades y el catálogo de estatus
             * son consultas independientes, por lo que
             * pueden ejecutarse en paralelo.
             */
            var continuidadesTask = _continuidadProxy.GetContinuidadesPorPeriodoAsync(fechaInicio,fechaFin);
            var estatusTask = _estatusProxy.GetAllEstatus();

            await Task.WhenAll(continuidadesTask,estatusTask);

            var continuidades = await continuidadesTask;

            var catalogoEstatus = await estatusTask;

            /*
             * Si no existen continuidades en el periodo,
             * se construye el dashboard respetando el
             * catálogo de estatus actual.
             */
            if (continuidades.Count == 0)
            {
                return CrearDashboardVacio(
                    filtro,
                    catalogoEstatus);
            }

            /*
             * Se eliminan expedientes duplicados para evitar
             * consultar repetidamente la información de DGRH.
             */
            var expedientes = continuidades.Select(c => c.Expediente).Where(e => e > 0).Distinct().ToArray();

            var puestos = await _empleadoProxy.GetUltimosPuestosAsync(expedientes);

            /*
             * Diccionario para obtener el nivel por expediente
             * en tiempo O(1).
             */
            var nivelPorExpediente = puestos.GroupBy(p => p.Expediente).ToDictionary(g => g.Key,g => g.First().Nivel);

            /*
             * Construcción de la información utilizada
             * exclusivamente por el Dashboard.
             */
            var datos = continuidades.Where(c => c.FechaCreacion.HasValue).Select(c =>
            {
                nivelPorExpediente.TryGetValue(c.Expediente,out var nivel);
                var tipoPersonal = TipoPersonalHelper.ObtenerTipo(nivel);
                return new ContinuidadDashboardItem
                {
                    Id = c.Id,
                    Expediente = c.Expediente,
                    EstatusId = c.EstatusId,
                    FechaCreacion = c.FechaCreacion!.Value,
                    Pagado = c.Pagado.GetValueOrDefault(),
                    Nivel = nivel ?? string.Empty,
                    TipoPersonal = tipoPersonal
                };
            }).ToList();

            datos = AplicarFiltros(datos,filtro);

            var resumenEstatus = ConstruirResumenEstatus(datos,catalogoEstatus);
            
            var semanas = ConstruirSemanas(datos,catalogoEstatus,filtro.Anio,filtro.Mes);

            return new DashboardContinuidadesDto
            {
                Anio = filtro.Anio,
                Mes = filtro.Mes,
                Periodo = ObtenerNombrePeriodo(filtro.Anio,filtro.Mes),
                Totales = ConstruirTotales(datos),
                Semanas = semanas,
                Estatus = resumenEstatus
            };
        }

        #region Validaciones

        private static void ValidarFiltro(DashboardFiltroDto filtro)
        {
            ArgumentNullException.ThrowIfNull(filtro);

            if (filtro.Anio < 2000 || filtro.Anio > 2100)
            {
                throw new ArgumentOutOfRangeException(nameof(filtro.Anio),"El año especificado no es válido.");
            }

            if (filtro.Mes is < 1 or > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(filtro.Mes),"El mes debe encontrarse entre 1 y 12.");
            }
        }

        #endregion

        #region Filtros

        private static List<ContinuidadDashboardItem> AplicarFiltros(List<ContinuidadDashboardItem> datos,DashboardFiltroDto filtro)
        {
            IEnumerable<ContinuidadDashboardItem> query = datos;

            if (!string.IsNullOrWhiteSpace(filtro.TipoPersonal))
            {
                query = query.Where(d => string.Equals(d.TipoPersonal,filtro.TipoPersonal,StringComparison.OrdinalIgnoreCase));
            }

            if (filtro.EstatusId.HasValue)
            {
                query = query.Where(d => d.EstatusId == filtro.EstatusId.Value);
            }

            return query.ToList();
        }

        #endregion

        #region Semanas

        private static List<DashboardSemanaDto> ConstruirSemanas(IReadOnlyCollection<ContinuidadDashboardItem> datos, 
            IReadOnlyCollection<EstatusContinuidadDto> catalogo, int anio, int mes)
        {
            var rangos = PeriodoSemanaHelper.ObtenerSemanas(anio,mes);

            return rangos.Select(rango =>
            {
                var fechaFinExclusiva = rango.FechaFin.AddDays(1);
                var solicitudesSemana = datos.Where(d => d.FechaCreacion >= rango.FechaInicio && d.FechaCreacion < fechaFinExclusiva).ToList();
                
                var estatus = ConstruirResumenEstatus(solicitudesSemana,catalogo);
                return new DashboardSemanaDto
                {
                    Numero = rango.Numero,
                    FechaInicio = rango.FechaInicio,
                    FechaFin = rango.FechaFin,
                    Descripcion = CrearDescripcionSemana(rango),
                    IngresadasOperativo = solicitudesSemana.Count(d => d.TipoPersonal == TipoPersonalHelper.Operativo),
                    IngresadasMandoMedio = solicitudesSemana.Count(d => d.TipoPersonal == TipoPersonalHelper.MandoMedio),
                    IngresadasMandoSuperior = solicitudesSemana.Count(d => d.TipoPersonal == TipoPersonalHelper.MandoSuperior),
                    Estatus = estatus
                };
            }).ToList();
        }

        private static string CrearDescripcionSemana(RangoSemana rango)
        {
            var cultura = CultureInfo.GetCultureInfo("es-MX");
            if (rango.FechaInicio.Date == rango.FechaFin.Date)
            {
                return rango.FechaInicio.ToString("dd 'de' MMMM",cultura);
            }
            return $"{rango.FechaInicio:dd} - " + $"{rango.FechaFin.ToString("dd 'de' MMMM",cultura)}";
        }

        #endregion

        #region Estatus

        private static List<DashboardEstatusDto> ConstruirResumenEstatus(IReadOnlyCollection<ContinuidadDashboardItem> datos,
            IEnumerable<EstatusContinuidadDto> catalogo)
        {
            var datosPorEstatus = datos.GroupBy(d => d.EstatusId).ToDictionary(g => g.Key,g => g.ToList());
            
            return catalogo.OrderBy(e => e.Orden).Select(estatus =>
            {
                datosPorEstatus.TryGetValue(estatus.Id,out var solicitudes);
                solicitudes ??= new List<ContinuidadDashboardItem>();
                return new DashboardEstatusDto
                {
                    EstatusId = estatus.Id,
                    Nombre = estatus.Nombre ?? string.Empty,
                    Abreviacion = estatus.Abreviacion ?? string.Empty,
                    FondoHexadecimal = estatus.FondoHexadecimal,
                    Total = solicitudes.Count,
                    Operativo = solicitudes.Count(d => d.TipoPersonal == TipoPersonalHelper.Operativo),
                    MandoMedio = solicitudes.Count(d => d.TipoPersonal == TipoPersonalHelper.MandoMedio),
                    MandoSuperior = solicitudes.Count(d => d.TipoPersonal == TipoPersonalHelper.MandoSuperior),
                };
            }).ToList();
        }

        #endregion

        #region Totales

        /// <summary>
        /// Contiene únicamente métricas generales que
        /// NO dependen del catálogo de estatus.
        /// </summary>
        private static DashboardTotalesDto ConstruirTotales(IReadOnlyCollection<ContinuidadDashboardItem> datos)
        {
            return new DashboardTotalesDto
            {
                Ingresadas = datos.Count,
                Pagadas = datos.Count(d => d.Pagado)
            };
        }

        #endregion

        #region Dashboard vacío

        private static DashboardContinuidadesDto CrearDashboardVacio(DashboardFiltroDto filtro, IReadOnlyCollection<EstatusContinuidadDto> catalogo)
        {
            /*
             * Aunque no haya solicitudes, conservamos
             * los estatus del catálogo.
             */
            var estatusVacios = ConstruirResumenEstatus(Array.Empty<ContinuidadDashboardItem>(),catalogo);
            var semanas = PeriodoSemanaHelper.ObtenerSemanas(filtro.Anio,filtro.Mes).Select(rango => new DashboardSemanaDto 
            {
                Numero = rango.Numero,
                FechaInicio = rango.FechaInicio,
                FechaFin = rango.FechaFin,
                Descripcion = CrearDescripcionSemana(rango),
                IngresadasOperativo = 0,
                IngresadasMandoMedio = 0,
                IngresadasMandoSuperior = 0,
                Estatus = ConstruirResumenEstatus(Array.Empty<ContinuidadDashboardItem>(),catalogo)
            }).ToList();
            
            return new DashboardContinuidadesDto
            {
                Anio = filtro.Anio,
                Mes = filtro.Mes,
                Periodo = ObtenerNombrePeriodo(filtro.Anio,filtro.Mes),
                Totales = new DashboardTotalesDto(),
                Semanas = semanas,
                Estatus = estatusVacios
            };
        }

        #endregion

        #region Periodo

        private static string ObtenerNombrePeriodo(int anio,int mes)
        {
            var cultura = CultureInfo.GetCultureInfo("es-MX");
            var fecha = new DateTime(anio,mes,1);
            var nombreMes = fecha.ToString("MMMM",cultura);
            nombreMes = char.ToUpper(nombreMes[0],cultura) +nombreMes[1..];
            return $"{nombreMes} {anio}";
        }

        #endregion
    }
}