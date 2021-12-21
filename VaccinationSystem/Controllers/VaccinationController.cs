using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VaccinationSystem.Controllers
{
    public class VaccinationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        
        public IActionResult Authenticate()
        {

            var UserClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Ritu"),
                new Claim(ClaimTypes.Email, "ritumj406@gmail.com"),
            };

            var UserIdentity = new ClaimsIdentity(UserClaims, "User Identity");
            var userPrincipal = new ClaimsPrincipal(UserIdentity);
            HttpContext.SignInAsync(userPrincipal);
           return RedirectToAction("Index");
        }
    }
}
