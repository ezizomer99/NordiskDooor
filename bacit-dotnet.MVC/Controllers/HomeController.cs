using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Models.Users;
using bacit_dotnet.MVC.Repositories;
using bacit_dotnet.MVC.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bacit_dotnet.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;
        private UserList userList = new UserList();

        public HomeController( IUserRepository userRepository)
        {
            
            this.userRepository = userRepository;
            userList.Users = userRepository.GetUsers();
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Validate(string employeeNumber, string password, string returnUrl)
        {
            if (password == null || employeeNumber == null)
            {
                TempData["Error"] = "Venligst fyll inn alle felt!";
                return RedirectToAction("Login");
            }
            var user = userList.GetUser(employeeNumber);
            if (!(user == null)) //if user = null vil ikke neste if setning kunne fullføres derfor er denne her 
            {
                ViewData["returnUrl"] = returnUrl;
                if (user.Password.Equals(EncryptString.Encrypt(password)))
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, employeeNumber));
                    claims.Add(new Claim(ClaimTypes.Name, user.Name));
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    if (user.IsAdmin)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    }
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Suggestions");
                }
                else
                {
                    TempData["Error"] = "Error. Employee number or password is wrong";
                    return View("login", returnUrl);
                }
            }
            else
            {
                TempData["Error"] = "Error. Employee number or password is wrong";
                return View("login", returnUrl);
            }
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
            
        }

        [HttpGet("Denied")]
        public IActionResult Denied()
        {
            return View();
        }
    }
}