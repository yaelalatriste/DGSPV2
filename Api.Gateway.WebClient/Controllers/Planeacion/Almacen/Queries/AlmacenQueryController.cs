using Api.Gateway.Models.Catalogos.DTOs.CTArticulos;
using Api.Gateway.Models.Planeacion.Queries.Almacen;
using Api.Gateway.Proxies.Catalogos.CTArticulos.Queries;
using Api.Gateway.Proxies.Catalogos.CTCategorias.Queries;
using Api.Gateway.Proxies.Catalogos.CTUnidades.Queries;
using Api.Gateway.Proxies.Planeacion.Queries.Almacen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers.Planeacion.Almacen.Queries
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("planeacion/almacen")]
    public class AlmacenQueryController : ControllerBase
    {
        private readonly IQAlmacenProxy _almacen;
        private readonly IQCTArticuloProxy _articulos;
        private readonly IQCTCategoriaProxy _categorias;
        private readonly IQCTUnidadProxy _unidades;

        public AlmacenQueryController(IQAlmacenProxy almacen, IQCTArticuloProxy articulos, IQCTCategoriaProxy categorias, IQCTUnidadProxy unidades)
        {
            _almacen = almacen;
            _articulos = articulos;
            _categorias = categorias;
            _unidades = unidades;
        }

        [Route("getAlmacenClasificado")]
        [HttpGet]
        public async Task<List<CTArticuloCDto>> GetArticulosAgrupados()
        {
            var almacn = await _almacen.GetAllAlmacenAsync();
            var unidades = await _unidades.GetAllUnidades();
            var articulos = await _articulos.GetAllArticulosAsync();

            var almacen = almacn.GroupJoin(articulos, a => a.ArticuloId, ar => ar.Id, (x, y) => new { almacn = x, articulos = y })
                                .SelectMany(x => x.articulos.DefaultIfEmpty(), (x, y) => new { x.almacn, articulos = y })
                                .GroupJoin(unidades, a => a.almacn.UnidadId, u => u.Id, (x, y) => new { almacn = x, unidades = y })
                                .SelectMany(x => x.unidades.DefaultIfEmpty(), (x, z) => new { x.almacn, unidades = z })
                                .Select(a => new AlmacenDto
                                {
                                    ArticuloId = a.almacn.articulos.Id,
                                    Cantidad = a.almacn.almacn.Cantidad,
                                    UnidadId = a.almacn.almacn.UnidadId,
                                    Unidad = a.unidades.Nombre,
                                    Articulo = a.almacn.articulos.Nombre,
                                    CategoriaId = a.almacn.articulos.CategoriaId,
                                    FechaActualizacion = a.almacn.almacn.FechaActualizacion
                                }).ToList();

            var carticulos = almacen.GroupBy(a => new { a.CategoriaId }).Select(a => new CTArticuloCDto
            {
                CategoriaId = a.Key.CategoriaId,
                TotalElementos = a.Sum(a => a.Cantidad)
            }).ToList();

            foreach (var at in carticulos)
            {
                at.Categoria = await _categorias.GetCategoriaByIdAsync(at.CategoriaId);
            }

            return carticulos;
        }

        [Route("getAlmacen")]
        [HttpGet]
        public async Task<List<AlmacenDto>> GetAlmacen()
        {
            var almacn = await _almacen.GetAllAlmacenAsync();
            var unidades = await _unidades.GetAllUnidades();
            var articulos = await _articulos.GetAllArticulosAsync();

            var almacen = almacn.GroupJoin(articulos, a => a.ArticuloId, ar => ar.Id, (x, y) => new { almacn = x, articulos = y })
                                .SelectMany(x => x.articulos.DefaultIfEmpty(), (x, y) => new { x.almacn, articulos = y })
                                .GroupJoin(unidades, a => a.almacn.UnidadId, u => u.Id, (x, y) => new { almacn = x, unidades = y })
                                .SelectMany(x => x.unidades.DefaultIfEmpty(), (x, z) => new { x.almacn, unidades = z })
                                .Select(a => new AlmacenDto
                                {
                                    ArticuloId = a.almacn.articulos.Id,
                                    Cantidad = a.almacn.almacn.Cantidad,
                                    UnidadId = a.almacn.almacn.UnidadId,
                                    Unidad = a.unidades.Nombre,
                                    Articulo = a.almacn.articulos.Nombre,
                                    CategoriaId = a.almacn.articulos.CategoriaId,
                                    FechaActualizacion = a.almacn.almacn.FechaActualizacion
                                }).ToList();


            foreach (var at in almacen)
            {
                at.Categoria = (await _categorias.GetCategoriaByIdAsync(at.CategoriaId)).Nombre;
            }

            return almacen;
        }
    }
}
