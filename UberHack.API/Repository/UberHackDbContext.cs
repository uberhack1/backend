﻿using Microsoft.EntityFrameworkCore;
using UberHack.API.Entities;
using UberHack.API.Repository.Configuration;

namespace UberHack.API.Repository
{
    public class UberHackDbContext : DbContext
    {
        public DbSet<Bairro> Bairro { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<ChatUsuarios> ChatUsuarios { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Faculdade> Faculdade { get; set; }
        public DbSet<Mensagem> Mensagem { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        public UberHackDbContext(DbContextOptions<UberHackDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=ec2-23-21-160-38.compute-1.amazonaws.com;Database=d53d9j5genek5a;Username=hctvaqyhesrgdr;Password=d35ab130a5d647084cd42b9d2f4317514adc39cb03855e0c00601e95b065d7e8;sslmode=Require;Trust Server Certificate=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsuarioConfiguration).Assembly);
        }
    }

}
