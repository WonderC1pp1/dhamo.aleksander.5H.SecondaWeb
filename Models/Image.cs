
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace dhamo.aleksander._5H.SecondaWeb.Models
{
    public class Image
    {
        [Key]
        public int idImage {get; set;}
        public string  linkImmagine {get; set;}
        public string Titolo {get; set;} 
        public string Descrizione {get; set;}


        //fk tabella utenti
        public string idUtente {get; set;}

    }
    
}