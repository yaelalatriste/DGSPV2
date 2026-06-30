using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Catalogos;

namespace DGSP.Module.Seguros.Application.Services.CJFBDRHDF.Calculadora
{
    public interface ICatalogosSgmmService
    {
        Task<CatalogosSgmmDto> ObtenerCatalogosSgmm(ObtenerCatalogosSgmmDto catalogo);
    }
}
