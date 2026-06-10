using DGSP.Components.Common.Interfaces.Generales;
using DGSP.Infraestructure.Data.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Infraestructure
{
    public class RepositorioCatalogos<T> : IRepositorio<T> where T : class
    {
        private readonly CatalogoDbContext _catalogos;

        public RepositorioCatalogos(CatalogoDbContext catalogos)
        {
            _catalogos = catalogos;
        }

        public Task Actualizar(T entidad)
        {
            _catalogos.Update(entidad);
            return Task.CompletedTask;
        }

        public Task<T> Agregar(T entidad)
        {
            _catalogos.Add(entidad);
            return Task.FromResult(entidad);
        }

        public Task Borrar(T entidad)
        {
            _catalogos.Remove(entidad);
            return Task.CompletedTask;
        }

        public async Task<T?> ObtenerPorId(int id)
        {
            return await _catalogos.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> ObtenerTodos()
        {
            return await _catalogos.Set<T>().ToListAsync();
        }

    }
}
