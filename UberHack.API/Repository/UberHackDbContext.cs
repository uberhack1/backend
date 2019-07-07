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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=ec2-23-21-160-38.compute-1.amazonaws.com;Database=d53d9j5genek5a;Username=hctvaqyhesrgdr;Password=d35ab130a5d647084cd42b9d2f4317514adc39cb03855e0c00601e95b065d7e8");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsuarioConfiguration).Assembly);
        }
    }

}
