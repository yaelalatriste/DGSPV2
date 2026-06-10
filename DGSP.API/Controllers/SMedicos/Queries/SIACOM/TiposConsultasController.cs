using DGSP.Module.SMedicos.Application.Interfaces.Siacom.TipoConsulta;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMedicos.Services.Queries.Queries.Siacom.TiposConsulta;

namespace DGSP.API.Controllers.SMedicos.Queries.SIACOM;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/smedicos/[controller]")]
public class TiposConsultasController : ControllerBase
{
    private readonly ITipoConsultaQueryService _tipoConsulta;

    public TiposConsultasController(ITipoConsultaQueryService tipoConsulta)
    {
        _tipoConsulta = tipoConsulta;
    }

    [HttpGet]
    [Route("getAllTiposConsultas")]
    public async Task<IActionResult> GetAllTiposConsultas()
    {
        var tiposConsultas = await _tipoConsulta.GetAllTiposConsultas();

        return Ok(tiposConsultas);
    }

    [HttpGet]
    [Route("getTipoConsultaById/{id}")]
    public async Task<IActionResult> GetTipoConsultaById(int id)
    {
        var tipoConsulta = await _tipoConsulta.GetTipoConsultaById(id);

        return Ok(tipoConsulta);
    }
}