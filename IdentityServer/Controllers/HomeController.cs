using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signinManager;

        //Microsoft provides usermanager, SignIn Manager which we are making use of
        public HomeController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signManager)
        {
            _userManager = userManager;
            _signinManager = signManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user!= null)
            {
                //sign in 
                var signInResult = await _signinManager.PasswordSignInAsync(user, password, false, false);
                if(signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = ""

            };

            //Creating a record in the DB for the user
            var result = await _userManager.CreateAsync(user,password);
            if(result.Succeeded)
            {
                //sign user in 

                //sign in 
                var signInResult = await _signinManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Login()
        {
            //login functionality
            return View(); 
        }

        public IActionResult Register()
        {

            return View();
        }

        public async Task<IActionResult> Logout()
        {
           await  _signinManager.SignOutAsync();
            return RedirectToAction("Index");
        }

    }
}
