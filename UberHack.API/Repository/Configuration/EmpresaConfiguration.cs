using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UberHack.API.Entities;

namespace UberHack.API.Repository.Configuration
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(o => o.Nome);
        }
    }
}