using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using UberHack.API.Entities;

namespace UberHack.API.Repository.Configuration
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(o => o.Nome);

            builder.HasMany(o => o.Mensagens);
            builder.HasMany(o => o.ChatUsuarios);
        }
    }
}