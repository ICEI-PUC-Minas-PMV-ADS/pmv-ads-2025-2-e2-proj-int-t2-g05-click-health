using Microsoft.EntityFrameworkCore;

namespace mf_dev_backend_2025.Models
{
    public class AppBbContex : DbContext
    {
        public AppBbContex(DbContextOptions<AppBbContex> options) : base(options) { }

        public DbSet<novopaciente> novospacientes { get; set; } = null!;


    }
}
