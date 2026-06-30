using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.SGMM;

namespace DGSP.Module.Seguros.Application.Services.CJFBDRHDF.SGMM
{
    public interface ServidorPublicoOpMMSService
    {
        Task<ServidorPublicoOpMMSDto> GetServidorPublicoOpMMS(int expediente);
    }
}
