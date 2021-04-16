using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dhamo.aleksander._5H.SecondaWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace dhamo.aleksander._5H.SecondaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        [HttpPost]
        public IActionResult Prenota(Prenotazione p)
        {
            //tipo - etichetta - operatore - valore - terminatore di istruzione
            var db=new PrenotazioneContext(); //oppure PrenotazioneContext db=new PrenotazioneContext(); 
            db.Prenotazioni.Add(p);
            db.SaveChanges();
            return View("Elenco", db);
        }


        //cancella prenotazione
        public IActionResult Cancella(int id)
        {
            var db=new PrenotazioneContext();
            Prenotazione prenotazione = db.Prenotazioni.Find(id);
            if(prenotazione!=null)
            {
                db.Remove(prenotazione);            
                db.SaveChanges();
                return View("Elenco",db);
            }else{
                return NotFound();
            }
            
        }

        [HttpGet]
        public IActionResult Modifica(int id)
        {
            var dbds=new DBContext();
            var db=new PrenotazioneContext();
            Prenotazione prenotazione = db.Prenotazioni.Find(id);
            if(prenotazione!=null)
            {
                return View("Modifica",prenotazione);
            }
            else{
                return NotFound();                
            }
            
        }

        [HttpPost]
        public IActionResult Modifica(int id, Prenotazione newPrenotazione)
        {
            var db=new PrenotazioneContext();
            Prenotazione prenotazione = db.Prenotazioni.Find(id);
            if(prenotazione!=null)
            {
                prenotazione.Nome=newPrenotazione.Nome;
                prenotazione.Email=newPrenotazione.Email;
                prenotazione.Telefono=newPrenotazione.Telefono;
                db.Prenotazioni.Update(prenotazione);
                db.SaveChanges();
            }
            return View("Elenco",db);
        }

        public IActionResult CancellaTutto()
        {   
            var db=new PrenotazioneContext();
            db.RemoveRange(db.Prenotazioni);
            
            //Prenotazione prenotazione = db.Prenotazioni.Find(id);
            //db.Remove(prenotazione);
            db.SaveChanges();
            return View("Elenco",db);
        }      

        
        [HttpPost]
        public IActionResult Upload(CreatePost post)
        {
            MemoryStream memStream=new MemoryStream();
            post.MyCSV.CopyTo(memStream);
            //mette a zero il puntatore dello StreamReader
            memStream.Seek(0,0);

            StreamReader fim=new StreamReader(memStream);
            if(!fim.EndOfStream)
            {
                //accedi al database
                var db=new PrenotazioneContext(); //oppure PrenotazioneContext db=new PrenotazioneContext(); 
                string riga = fim.ReadLine();
                while(!fim.EndOfStream)
                {
                    riga = fim.ReadLine();
                    string[] colonne = riga.Split(";");
                    Prenotazione p= new Prenotazione{Nome=colonne[0], Email=colonne[1], Telefono=colonne[2], Partecipazione=Convert.ToBoolean(colonne[3])};
                    
                    db.Prenotazioni.Add(p);
                }                
                db.SaveChanges();
            
                return View("Elenco", db);                
            }         
            return View();
        }  
        
    }
}
