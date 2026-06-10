using DGSP.Components.Common.Interfaces.Generales;
using DGSP.Module.SMedicos.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Infraestructure
{
    public class RepositorioSMedicos<T> : IRepositorio<T> where T : class
    {
        private readonly SMedicosDbContext _smedicos;

        public RepositorioSMedicos(SMedicosDbContext smedicos)
        {
            _smedicos = smedicos;
        }

        public Task Actualizar(T entidad)
        {
            _smedicos.Update(entidad);
            return Task.CompletedTask;
        }

        public Task<T> Agregar(T entidad)
        {
            _smedicos.Add(entidad);
            return Task.FromResult(entidad);
        }

        public Task Borrar(T entidad)
        {
            _smedicos.Remove(entidad);
            return Task.CompletedTask;
        }

        public async Task<T?> ObtenerPorId(int id)
        {
            return await _smedicos.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> ObtenerTodos()
        {
            return await _smedicos.Set<T>().ToListAsync();
        }

    }
}
