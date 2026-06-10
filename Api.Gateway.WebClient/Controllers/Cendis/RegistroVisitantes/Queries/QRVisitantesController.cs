using Api.Gateway.Proxies.Catalogos.CTCendis.Queries;
using Api.Gateway.Proxies.Cendis.Queries.RegistroVisitantes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Cendis.RegistroVisitantes.Queries
{
    [ApiController]
    [Route("cendis/registroVisitantes")]
    public class QRVisitantesController : ControllerBase
    {
        private readonly IQRVisitantesCendisProxy  _registros;
        private readonly IQCTCendisProxy _cendis;

        public QRVisitantesController(IQRVisitantesCendisProxy registros, IQCTCendisProxy cendis)
        {
            _registros = registros;
            _cendis = cendis;
        }

        [HttpGet]
        [Route("getAllRVisitantes")]
        public async Task<IActionResult> GetAllRVisitantes()
        {
            var expedientes = await _registros.GetAllRVisitantes();
            foreach (var e in expedientes)
            {
                e.Cendi = await _cendis.GetCendisByIdAsync(e.CendiId);
            }

            return Ok(expedientes);
        }

        [HttpGet]
        [Route("getRegistrosByDia/{dia}")]
        public async Task<IActionResult> GetRegistrosByDia(string dia)
        {
            var Expedientes = await _registros.GetRVisitantsByDay(dia);

            return Ok(Expedientes);
        }
       
    }
}
