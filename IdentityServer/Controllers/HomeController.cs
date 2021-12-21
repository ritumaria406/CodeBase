using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
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
        private readonly IEmailService _emailService;

        //Microsoft provides usermanager, SignIn Manager which we are making use of
        public HomeController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signManager,IEmailService emailService)
        {
            _userManager = userManager;
            _signinManager = signManager;
            _emailService = emailService;

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

                //Generate email verification token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var link = Url.Action(nameof(VerifyEmail), "Home", new { user = user.Id, code = code },Request.Scheme, Request.Host.ToString());
                await _emailService.SendAsync("test@testco.com", "Email Verfication Link", $"<a href=\"{link}\"></a>",true);
                return RedirectToAction("EmailVerification");
                //If user is added we need to send email for verification

                //sign in 
               
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

        public IActionResult EmailVerification()
        {
            //login functionality
            return View();
        }
        
        public async Task<IActionResult> Logout()
        {
           await  _signinManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> VerifyEmail(string userId,string code)
        {
            var userDetails = await _userManager.FindByIdAsync(userId);
            if (userDetails == null) return BadRequest();
            var result = await _userManager.ConfirmEmailAsync(userDetails,code);
            if(result.Succeeded)
            {
                return View();
            }
            return BadRequest();
        }

    }
}
