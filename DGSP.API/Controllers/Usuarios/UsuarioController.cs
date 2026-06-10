using DGSP.Shared.Contracts.DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Service.Queries.Queries;

namespace DGSP.API.Controllers.Usuarios
{
    [ApiController]
    [Route("sausuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioQueryService _usuarios;

        public UsuarioController(IUsuarioQueryService usuarios)
        {
            _usuarios = usuarios;
        }
    }
}
