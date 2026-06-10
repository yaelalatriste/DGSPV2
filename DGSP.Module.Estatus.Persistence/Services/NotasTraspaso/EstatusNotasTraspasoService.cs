using DGSP.Module.Estatus.Application.Interfaces.NotasTraspaso;
using DGSP.Module.Estatus.Application.Services.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.Estatus.NotasTraspaso;

namespace DGSP.Module.Estatus.Persistence.Services.NotasTraspaso
{
    public class EstatusNotasTraspasoService : IEstatusNotasTraspasoService
    {
        private readonly IEstatusNotasTraspasoRepository _estatusNotasTraspasoRepository;

        public EstatusNotasTraspasoService(IEstatusNotasTraspasoRepository estatusNotasTraspasoRepository)
        {
            _estatusNotasTraspasoRepository = estatusNotasTraspasoRepository;
        }

        public async Task<List<ENotaTraspasoDto>> GetAllEstatus()
        {
            var estatus = await _estatusNotasTraspasoRepository.GetAllEstatus();

            return estatus.Select(e => new ENotaTraspasoDto {
                Id = e.Id,
                Abreviacion = e.Abreviacion,
                Nombre = e.Nombre,
                Orden = e.Orden,
                Editar = e.Editar,
                MemorandumPdf = e.MemorandumPdf,
                MemorandumWord = e.MemorandumWord,
                FechaCreacion = e.FechaCreacion,
            }).ToList();

        }

        public async Task<ENotaTraspasoDto> GetEstatusById(int id)
        {
            var estatus = await _estatusNotasTraspasoRepository.GetEstatusById(id);

            return new ENotaTraspasoDto {
                Id = estatus.Id,
                Abreviacion = estatus.Abreviacion,
                Nombre = estatus.Nombre,
                Orden = estatus.Orden,
                Editar = estatus.Editar,
                MemorandumPdf = estatus.MemorandumPdf,
                MemorandumWord = estatus.MemorandumWord,
                FechaCreacion = estatus.FechaCreacion,
            };
        }
    }
}
