using Api.Gateway.Models.Catalogos.DTOs.CTCategorias;
using Api.Gateway.Proxies.Catalogos.CTArticulos.Queries;
using Api.Gateway.Proxies.Catalogos.CTCategorias.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Catalogos.CTCategorias
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("catalogos/categorias")]
    public class CTCategoriaController : ControllerBase
    {
        private readonly IQCTCategoriaProxy _categorias;
        private readonly IQCTArticuloProxy _articulos;

        public CTCategoriaController(IQCTCategoriaProxy categorias, IQCTArticuloProxy articulos)
        {
            _categorias = categorias;
            _articulos = articulos;
        }


        [HttpGet]
        [Route("getCategoriasByTipo/{tipo}")]
        public async Task<List<CTCategoriaDto>> getCategoriasById(string tipo)
        {
            List<CTCategoriaDto> categorias = await _categorias.GetCategoriasByTipo(tipo);
            return categorias;
        }

        [Route("getCategoriasWithArticulos")]
        [HttpGet]
        public async Task<List<CTCategoriaDto>> GetCategoriasWithArticulos()
        {
            var categorias = await _categorias.GetAllCategoriasAsync();

            foreach (var ct in categorias)
            {
                ct.Articulos = (await _articulos.GetArticulosByCategoriaAsync(ct.Id)).ToList();
            }

            return categorias;
        }
    }
}
