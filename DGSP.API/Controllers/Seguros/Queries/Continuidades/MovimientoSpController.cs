using DGSP.Module.Estatus.Application.Services.Continuidades;
using DGSP.Module.Estatus.Domain;
using DGSP.Module.Seguros.Application.Services.CJFBDRHDF.SGMM;
using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Module.Seguros.Persistence.Services.CJFBDRHDF.Catalogos;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.Continuidad;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.OficiosContinuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Logs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGSP.API.Controllers.Seguros.Queries.Continuidades
{

    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/seguros/continuidades/[controller]")]
    public class MovimientoSpController : ControllerBase
    {
        private readonly IContinuidadService _continuidadService;
        private readonly IOficioContinuidadService _oficioContinuidadService;
        private readonly IMovimientoSpService _movimientoSpService;
        private readonly IMovimientoService _movimientoService;
        private readonly IEstatusContinuidadesService _estatusContinuidadesService;
        private readonly IMediator _mediator;

        public MovimientoSpController(IContinuidadService continuidadService, IMovimientoSpService movimientoSpService,
            IOficioContinuidadService oficioContinuidadService, IEstatusContinuidadesService estatusContinuidadesService,
            IMovimientoService movimientoService, IMediator mediator)
        {
            _continuidadService = continuidadService;
            _movimientoSpService = movimientoSpService;
            _movimientoService = movimientoService;
            _oficioContinuidadService = oficioContinuidadService;
            _estatusContinuidadesService = estatusContinuidadesService;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getMovimientosSp")]
        public async Task<IActionResult> GetMovimientosSpAsync()
        {
            var estatus = await _estatusContinuidadesService.GetAllEstatus();
            var continuidades = await _continuidadService.GetContinuidadesByEstatus(estatus.First(e => e.Abreviacion.Equals("EnMovimientos")).Id);
            foreach (var cn in continuidades)
            {
                var oficios = await _movimientoSpService.ObtenerMovimientoBajaAsync(cn);
                if (oficios != null && oficios.Count() != 0) 
                {
                    foreach (var of in oficios)
                    {
                        await _mediator.Send(of);
                    }

                    var oficiosCreados = await _oficioContinuidadService.GetOficiosByContinuidadAsync(cn.Id);

                    if (oficiosCreados.Count() != 0)
                    {
                        EstatusContinuidadCommand continuidad = new EstatusContinuidadCommand();
                        continuidad.Id = cn.Id;
                        continuidad.UsuarioId = cn.UsuarioId;
                        continuidad.EstatusId = estatus.First(c => c.Abreviacion.Equals("EnSolicitud")).Id;
                        var createLog = await _mediator.Send(continuidad);

                        RegistrarLogContinuidadCommand log = new RegistrarLogContinuidadCommand();
                        log.UsuarioId = cn.UsuarioId;
                        log.ContinuidadId = cn.Id;
                        log.EstatusId = estatus.First(c => c.Abreviacion.Equals("EnSolicitud")).Id;
                        log.Observaciones = "Se realizó el registro del oficio de baja por parte del área de Movimientos, lo cual permite continuar con el trámite de la continuidad del Seguro de Gastos Médicos Mayores (SGMM).";
                    }
                }
            }
            return Ok(continuidades); 
        }
        
        [HttpGet]
        [Route("getMovimientoSpByContinuidad/{continuidadId}")]
        public async Task<IActionResult> GetMovimientoSpByContinuidad(int continuidadId)
        {
            var estatus = await _estatusContinuidadesService.GetAllEstatus();
            var continuidad = await _continuidadService.GetContinuidadByIdAsync(continuidadId);
            int oficiosCreados = 0;

            if (continuidad != null)
            {
                var oficios = await _movimientoSpService.ObtenerMovimientoBajaAsync(continuidad);
                if (oficios != null && oficios.Count() != 0)
                {
                    foreach (var of in oficios)
                    {
                        await _mediator.Send(of);
                    }

                    oficiosCreados = (await _oficioContinuidadService.GetOficiosByContinuidadAsync(continuidad.Id)).Count();

                    if (oficiosCreados != 0)
                    {
                        EstatusContinuidadCommand estatusContinuidad = new EstatusContinuidadCommand();
                        estatusContinuidad.Id = continuidad.Id;
                        estatusContinuidad.UsuarioId = continuidad.UsuarioId;
                        estatusContinuidad.EstatusId = estatus.First(c => c.Abreviacion.Equals("EnSolicitud")).Id;

                        var createLog = await _mediator.Send(estatusContinuidad);
                        RegistrarLogContinuidadCommand log = new RegistrarLogContinuidadCommand();
                        log.UsuarioId = continuidad.UsuarioId;
                        log.ContinuidadId = continuidad.Id;
                        log.EstatusId = estatus.First(c => c.Abreviacion.Equals("EnSolicitud")).Id;
                        log.Observaciones = "Se realizó el registro del oficio de baja por parte del área de Movimientos, lo cual permite continuar con el trámite de la continuidad del Seguro de Gastos Médicos Mayores (SGMM).";

                        await _mediator.Send(log);
                    }
                }
            }

            return Ok(oficiosCreados);
        }
        
        [HttpGet]
        [Route("getMovimientoById/{id}")]
        public async Task<IActionResult> GetMovimientoById(int id)
        {
            var movimiento = await _movimientoService.GetMovimientosByIdAsync(id);

            return Ok(movimiento);
        }
    }
}
