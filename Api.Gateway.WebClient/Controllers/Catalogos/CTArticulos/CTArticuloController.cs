using Api.Gateway.Models.Catalogos.DTOs.CTArticulos;
using Api.Gateway.Proxies.Catalogos.CTArticulos.Queries;
using Api.Gateway.Proxies.Catalogos.CTCategorias.Queries;
using Api.Gateway.Proxies.Planeacion.Queries.Almacen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Catalogos.CTArticulos
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("catalogos/articulos")]
    public class CTArticuloController : ControllerBase
    {
        private readonly IQAlmacenProxy _almacen;
        private readonly IQCTArticuloProxy _articulos;
        private readonly IQCTCategoriaProxy _categorias;

        public CTArticuloController(IQAlmacenProxy almacen, IQCTArticuloProxy articulos, IQCTCategoriaProxy categorias)
        {
            _almacen = almacen;
            _articulos = articulos;
            _categorias = categorias;
        }

        [Route("getArticulos")]
        [HttpGet]
        public async Task<List<CTArticuloDto>> GetAllContratos()
        {
            var articulos = await _articulos.GetAllArticulosAsync();
            foreach (var at in articulos)
            {
                at.Categoria = await _categorias.GetCategoriaByIdAsync(at.CategoriaId);
            }
            return articulos;
        }

        [Route("getArticulosByCategoria/{categoria}")]
        [HttpGet]
        public async Task<List<CTArticuloDto>> GetArticulosByCategoria(int categoria)
        {
            var articulos = await _articulos.GetArticulosByCategoriaAsync(categoria);
            return articulos;
        }
    }
}
