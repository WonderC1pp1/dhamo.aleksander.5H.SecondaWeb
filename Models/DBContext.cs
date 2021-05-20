using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace dhamo.aleksander._5H.SecondaWeb.Models
{
    public class DBContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public DBContext(DbContextOptions options): base(options)
        {
            _options = options; 
        }

        public DBContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=database.db");

        public DbSet<Prenotazione> Prenotazioni { get ; set; }
        public DbSet<Image> Immagini { get ; set; }
    }
}