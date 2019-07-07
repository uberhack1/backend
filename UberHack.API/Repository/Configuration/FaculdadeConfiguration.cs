using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UberHack.API.Entities;

namespace UberHack.API.Repository.Configuration
{
    public class FaculdadeConfiguration : IEntityTypeConfiguration<Faculdade>
    {
        public void Configure(EntityTypeBuilder<Faculdade> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(o => o.Nome);
        }
    }
}