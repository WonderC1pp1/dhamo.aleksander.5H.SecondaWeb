using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dhamo.aleksander._5H.SecondaWeb.dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using dhamo.aleksander._5H.SecondaWeb.Models;
using Microsoft.AspNetCore.Http;

namespace dhamo.aleksander._5H.SecondaWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Accedi()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registra()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registra(RegistraDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                     //await _signInManager.SignInAsync(user, isPersistent: false);
                     return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Accedi(LoginDto user)
        {
            if (ModelState.IsValid)
            {
               var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
               if (result.Succeeded) {
                   // Se l'utente fa login correttamente, entra.
                   return RedirectToAction("Index", "Home");
               }
               else{

                   // ...altrimenti meglio non dare troppo info a chi ci prova
                   // meglio un generico errore,
                   ModelState.AddModelError(string.Empty, "Login error");
               }
            }

            return View(user); 

            
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        ///////////////////////////////////////////////////////
        
        [HttpGet] 
        public IActionResult Upload()
        {
            if( User.Identity.IsAuthenticated )
            {
                return View("~/Views/Home/Upload.cshtml");
            }
            return RedirectToAction("Accedi", "Account"); 
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if( User.Identity.IsAuthenticated )
            {
                return View("~/Views/Home/Privacy.cshtml");
            }
            return RedirectToAction("Accedi", "Account"); 
        }

        [HttpGet]
        public async Task<IActionResult> Elenco()
        {
            if( User.Identity.IsAuthenticated )
            {            
                
                IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);

                var db=new DBContext();

                HttpContext.Session.SetString("idUser", user.Id);

                //var immagini= (from s in db.Immagini where s.idUtente == $"{user.Id}" select s).ToArray();

                return View("~/Views/Home/Elenco.cshtml",db);
            }
            return RedirectToAction("Accedi", "Account");        
        }

        
        [HttpGet]
        public IActionResult Pubblica()
        {
            if( User.Identity.IsAuthenticated )
            {
                return View("~/Views/Home/Pubblica.cshtml");
            }
            return RedirectToAction("Accedi", "Account");
            
        }

        [HttpPost]
        public async Task<IActionResult> Pubblica(Image file)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            file.idUtente = user.Id;
            var db = new DBContext();
            db.Immagini.Add(file);
            db.SaveChanges();
            // file.idUtente= user
            return RedirectToAction("Elenco", "Account");
        }

        [HttpPost]
        public IActionResult Upload(Image file)
        {
            // var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            // file.idUtente = user.Id;
            // var db = new DBContext();
            // db.Immagini.Add(file);
            // // file.idUtente= user
            return View("Index");
        }

    }
}
