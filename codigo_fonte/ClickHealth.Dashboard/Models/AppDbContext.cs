using Microsoft.EntityFrameworkCore;

namespace ClickHealth.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<MedicamentoTomado> MedicamentosTomados { get; set; }
    }
}


