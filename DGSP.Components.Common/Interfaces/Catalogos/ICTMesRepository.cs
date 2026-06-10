using DGSP.Components.Common.Interfaces.Generales;
using DGSP.Domain.Catalogos.DMeses;

namespace DGSP.Components.Common.Interfaces.Catalogos
{
    public interface ICTMesRepository : IRepositorio<CTMes>
    {
        Task<List<CTMes>> GetAllMeses();
    }
}
