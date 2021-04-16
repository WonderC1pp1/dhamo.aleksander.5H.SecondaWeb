using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace dhamo.aleksander._5H.SecondaWeb.Models
{
    public class Prenotazione {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public bool? Partecipazione { get; set; } //il ? è una variabile nullabool, accetta il valore null (metodo HasValue dice se c'è il valore nella variabile)
    }
}