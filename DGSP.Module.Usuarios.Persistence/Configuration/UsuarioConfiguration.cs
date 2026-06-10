using DGSP.Module.Usuarios.Domain.DUsuarios;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DGSP.Module.Usuarios.Persistence.Configuration
{
    public class UsuarioConfiguration
    {
        public UsuarioConfiguration(EntityTypeBuilder<Usuario> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
        }
    }
}
