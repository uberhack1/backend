using Microsoft.EntityFrameworkCore;
using UberHack.API.Entities;
using UberHack.API.Repository.Configuration;

namespace UberHack.API.Repository
{
    public class UberHackDbContext : DbContext
    {
        public DbSet<Usuario> Funcionario { get; set; }

        public UberHackDbContext(DbContextOptions<UberHackDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsuarioConfiguration).Assembly);
        }
    }

}
