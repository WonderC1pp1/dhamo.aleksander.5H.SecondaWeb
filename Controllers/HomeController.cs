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
using Microsoft.AspNetCore.Hosting;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

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

        
        


        //cancella prenotazione
        public IActionResult Cancella(int id)
        {
            var db=new DBContext();
            Image immagine = db.Immagini.Find(id);
            if(immagine!=null)
            {
                db.Immagini.Remove(immagine);            
                db.SaveChanges();
                return View("~/Views/Home/Index.cshtml");
            }else{
                return NotFound();
            }
            
        }

        [HttpGet]
        public IActionResult Modifica(int id)
        {
            var db=new DBContext();
            Image immagine = db.Immagini.Find(id);
            if(immagine!=null)
            {
                return View("Modifica",immagine);
            }
            else{
                return NotFound();                
            }
            
        }

        [HttpPost]
        public IActionResult Modifica(Image newImage)
        {
            var db=new DBContext();
            Image oldImage = db.Immagini.Find(newImage.idImage);
            if(oldImage!=null)
            {
                oldImage.Descrizione= newImage.Descrizione;
                oldImage.linkImmagine= newImage.linkImmagine;
                oldImage.Titolo= newImage.Titolo;

                db.Immagini.Update(oldImage);
                db.SaveChanges();
            }
            return View("Elenco",db);
        }

        public IActionResult CancellaTutto()
        {   
            var db=new DBContext();
            db.RemoveRange(db.Immagini);
            
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
                var db=new DBContext(); //oppure PrenotazioneContext db=new PrenotazioneContext(); 
                string riga = fim.ReadLine();
                while(!fim.EndOfStream)
                {
                    riga = fim.ReadLine();
                    string[] colonne = riga.Split(";");
                    Image image= new Image{idUtente = this.HttpContext.Session.GetString("idUser"), Titolo= colonne[0], Descrizione=colonne[1], linkImmagine=colonne[2] };
                     
                    db.Immagini.Add(image);
                }                
                db.SaveChanges();
            
                return View("Elenco", db);                
            }         
            return View();
        }  

        
        

    }
}
