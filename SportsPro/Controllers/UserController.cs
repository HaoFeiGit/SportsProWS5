using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsPro.Controllers
{
    public class UserController : Controller
    {


        IUserManager _manager;

        public UserController(IUserManager manager)
        {
            _manager = manager;
        }

        // Route:   /User/Login
        public IActionResult Login(string returnUrl = null)
        {
            if (returnUrl != null)
                TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(User user)
        {
            //authenticate using the manager
            var usr = _manager.Authenticate(user.Username, user.Password);
            //return now if user object returned is null
            if (user == null) return View();
            //otherwise set up claims--one for each fact about the user
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usr.Username),
                new Claim("FullName", usr.FullName),
                new Claim(ClaimTypes.Role, usr.Role)
            };
            //create the instance of ClaimsIdentity (holds the claims)
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            //The signin creates the ClaimsPrincipal and cookie
            await HttpContext.SignInAsync("Cookies",
                                          new ClaimsPrincipal(claimsIdentity));
            //handle the return url value from TempData if it exists or not
            if (TempData["ReturnUrl"] == null)
                return RedirectToAction("Index", "Home");
            else
                return Redirect(TempData["ReturnUrl"].ToString());
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
