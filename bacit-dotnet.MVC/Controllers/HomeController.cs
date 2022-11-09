using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Models.Users;
using bacit_dotnet.MVC.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bacit_dotnet.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository userRepository;
        private UserList userList = new UserList();

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            this.userRepository = userRepository;
            userList.Users = userRepository.GetUsers();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new RazorViewModel
            {
                Content = "Nordic Door"
            };
            return View("Index", model);
        }

        [HttpGet("login")]
        public IActionResult login(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Validate(string employeeNumber, string password, string returnUrl)
        {
            var user = userList.GetUser(employeeNumber);
            if (!(user == null)) //if user = null vil ikke neste if setning kunne fullføres derfor er denne her 
            {
                ViewData["returnUrl"] = returnUrl;
                if (user.EmployeeNumber.Equals(employeeNumber) && user.Password.Equals(password))
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
                    return Redirect(returnUrl);
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