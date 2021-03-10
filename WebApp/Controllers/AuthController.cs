using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        MemberRepository repository;
        RoleRepository roleRepository;
        public AuthController(IConfiguration configuration)
        {
            repository = new MemberRepository(configuration);
            roleRepository = new RoleRepository(configuration);
        }
        public IActionResult Denied()
        {
            return View();
        }
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/auth/signin");
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Member obj)
        {
            repository.Add(obj);
            return RedirectToAction("signin");
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(SignInModel obj)
        {
            if (ModelState.IsValid)
            {
                Member member = repository.SignIn(obj);
                if (member != null)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, member.Username),
                        new Claim(ClaimTypes.NameIdentifier, member.ID.ToString()),
                        new Claim(ClaimTypes.Email, member.Email)
                    };
                    //roles
                    List<Role> roles = roleRepository.GetRoles(member.ID);
                    foreach (Role role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                    //xu ly login
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
                    //remember me
                    AuthenticationProperties properties = new AuthenticationProperties
                    {
                        IsPersistent = obj.Rem
                    };
                    HttpContext.SignInAsync(principal, properties);
                    return Redirect("/auth");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Sign in Failed");
                    
                }
            }
            return View(obj);
           
        }
    }
}
