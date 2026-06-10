using DGSP.Components.Common.Interfaces.Catalogos;
using DGSP.Domain.Catalogos.DMeses;
using DGSP.Infraestructure.Data.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Infraestructure.Repositories.Catalogos
{
    public class MesesRepository : RepositorioCatalogos<CTMes>, ICTMesRepository
    {
        private readonly CatalogoDbContext _catalogos;

        public MesesRepository(CatalogoDbContext catalogos) : base(catalogos)
        {
            _catalogos = catalogos;
        }

        public async Task<List<CTMes>> GetAllMeses()
        {
            var meses = await _catalogos.CTMeses.ToListAsync();

            return meses;
        }
    }
}
