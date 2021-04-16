using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dhamo.aleksander._5H.SecondaWeb.Models
{
    public class PrenotazioneContext : DbContext
    {
        public DbSet<Prenotazione> Prenotazioni { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=database.db");
    }
}