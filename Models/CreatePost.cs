
using Microsoft.AspNetCore.Http;

namespace dhamo.aleksander._5H.SecondaWeb.Models
{
    public class CreatePost
    {
        public string Descrizione {get;set;}
        public IFormFile MyCSV{get;set;}
    }
}