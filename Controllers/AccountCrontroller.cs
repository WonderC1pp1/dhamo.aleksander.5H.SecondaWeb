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
        public IActionResult Elenco()
        {
            if( User.Identity.IsAuthenticated )
            {
                

                var db=new PrenotazioneContext();
                return View("~/Views/Home/Elenco.cshtml",db);
            }
            return RedirectToAction("Accedi", "Account");        
        }

        [HttpGet]
        public IActionResult Prenota()
        {
            if( User.Identity.IsAuthenticated )
            {
                return View("~/Views/Home/Prenota.cshtml");
            }
            return RedirectToAction("Accedi", "Account");
            
        }

    }
}
