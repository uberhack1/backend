using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UberHack.API.Entities;

namespace UberHack.API.Repository.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(o => o.Nome);
            builder.Property(o => o.Bio);
            builder.Property(o => o.Cpf);
            builder.Property(o => o.Email);
            builder.Property(o => o.Foto);
            builder.Property(o => o.Senha);

            builder.HasOne(o => o.BairroCasa);
            builder.HasOne(o => o.Empresa);
            builder.HasOne(o => o.Faculdade);

            builder.HasMany(o => o.Chats);
        }
    }
}
