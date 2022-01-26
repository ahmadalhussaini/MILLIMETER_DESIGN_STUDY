using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MILLIMETER_DESIGN.Models.Repositories.Interface;
using MILLIMETER_DESIGN.ViweModel.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Controllers
{
    public class AdminController : Controller
    {
        private IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        [HttpGet]
        public IActionResult Login()
        
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViweModel model)
        {
            if (ModelState.IsValid)
            {

               var admin = _adminRepository.Login(model.Email, model.Password);
                if (admin == null)
                {
                    TempData["Error"] = "Username or Password is incorrect";
                    return View("Login");
                }
                var clamis = new List<Claim>();
               clamis.Add(new Claim("userId", admin.Id.ToString()));
                clamis.Add(new Claim("Email", admin.Email));
                clamis.Add(new Claim(ClaimTypes.NameIdentifier, admin.Email));
                var claimsIdentity = new ClaimsIdentity(clamis, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                var authProperties = new AuthenticationProperties
                {

                   ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                   IsPersistent = true,

                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

               return Redirect("/Project/Index");
           }
            TempData["Error"] = "Username or Password is incorrect";
            return RedirectToAction("Login");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
