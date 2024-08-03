using FastTypingApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Security.Claims;

namespace FastTypingApp.Controllers
{
    public class LoginController: Controller
    {
        AppDbContext dbContext;

        public LoginController(AppDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            var httpContext = Request.HttpContext;
            var form = httpContext.Request.Form;
            Console.WriteLine(form.ToString());
            string login = form["login"];
            string password = form["password"];

            var users = dbContext.Users;

            User? user = users.FirstOrDefault(u => u.Name == login && u.Password == HashUtils.sha256(password));
            if (user is null) {
               return RedirectToAction("Index");
            }
            var role = "user";
            if (user.IsAdmin)
            {
                role = "admin";
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await httpContext.SignInAsync(claimsPrincipal);
            //return RedirectToAction("Index");
            return Redirect("/");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            user.Password = HashUtils.sha256(user.Password);
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
