using DGSP.Gateway.Proxy.Queries.Estatus.Continuidades;
using DGSP.Gateway.Proxy.Services.Dashboards;
using DGSP.Shared.Contracts.DTOs.Estatus.Continuidades;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DashboardModel : PageModel
{
    private readonly IDashboardContinuidadesService _dashboardService;
    private readonly IQEstatusContinuidadProxy _estatusContinuidad;

    public DashboardModel(IDashboardContinuidadesService dashboardService, IQEstatusContinuidadProxy estatusContinuidad)
    {
        _dashboardService = dashboardService;
        _estatusContinuidad = estatusContinuidad;
    }

    public int AnioActual { get; private set; }
    public int MesActual { get; private set; }
    public List<EstatusContinuidadDto> Estatus { get; set; }

    public async Task OnGet()
    {
        var hoy = DateTime.Today;

        AnioActual = hoy.Year;
        MesActual = hoy.Month;
        Estatus = await _estatusContinuidad.GetAllEstatus();
    }

    public async Task<IActionResult> OnGetDatosAsync(int anio,int mes,string? tipoPersonal,int? estatusId)
    {
        try
        {
            var filtro = new DashboardFiltroDto
            {
                Anio = anio,
                Mes = mes,
                TipoPersonal = string.IsNullOrWhiteSpace(tipoPersonal) ? null : tipoPersonal,
                EstatusId = estatusId
            };
            
            var resultado = await _dashboardService.ObtenerDashboardAsync(filtro);

            return new JsonResult(resultado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(
                new
                {
                    mensaje = ex.Message
                });
        }
    }
}