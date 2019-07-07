using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UberHack.API.Entities;

namespace UberHack.API.Repository.Configuration
{
    public class BairroConfiguration : IEntityTypeConfiguration<Bairro>
    {
        public void Configure(EntityTypeBuilder<Bairro> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(o => o.Nome);
        }
    }
}