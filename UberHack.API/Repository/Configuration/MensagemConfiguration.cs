using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using UberHack.API.Entities;

namespace UberHack.API.Repository.Configuration
{
    public class MensagemConfiguration : IEntityTypeConfiguration<Mensagem>
    {
        public void Configure(EntityTypeBuilder<Mensagem> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(o => o.Conteudo);
            builder.Property(o => o.DataHora);

            builder.HasOne(o => o.Usuario);
            builder.HasOne(o => o.Chat);
        }
    }
}