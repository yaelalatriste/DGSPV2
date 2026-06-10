namespace DGSP.Components.Common.Interfaces.Generales
{
    public interface IRepositorio<T> where T : class
    {
        Task<T?> ObtenerPorId(int id);
        Task<IEnumerable<T>> ObtenerTodos();
        Task<T> Agregar(T entidad);
        Task Actualizar(T entidad);
        Task Borrar(T entidad);
    }
}
