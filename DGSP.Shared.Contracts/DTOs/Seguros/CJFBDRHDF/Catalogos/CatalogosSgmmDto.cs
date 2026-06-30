namespace DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Catalogos
{
    public class CatalogosSgmmDto
    {
        public List<CatalogoSimpleDto<byte>> TiposPoliza { get; set; } = new();
        public List<CatalogoSimpleDto<short>> Vigencias { get; set; } = new();
        public List<CatalogoSimpleDto<byte>> SumasBasicas { get; set; } = new();
        public List<CatalogoSimpleDto<byte>> SumasAseguradas { get; set; } = new();
        public List<CatalogoSimpleDto<byte>> ExtraPrimas { get; set; } = new();
        public List<CatalogoSimpleDto<short>> IQ { get; set; } = new();
        public List<CatalogoSimpleDto<byte>> Parentescos { get; set; } = new();
        public List<RangoEdadDto> RangosEdad { get; set; } = new();
    }
}
