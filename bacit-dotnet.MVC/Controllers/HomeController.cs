
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

        //Metode for å returnere view-et, er IActionResult som kan returnere  for eksempel View eller RedirectToAction 
        public IActionResult Index()
        {
            return View("Index");
        }

        //HtppGet gjør at det skjer en Get-request og viser view-et i /login (Forcer siden)
        [HttpGet("login")]
        public IActionResult Login()
        {
            
            return View();
        }
        //Hvis EmployeeNumber og Password er korrekt med det som er lagret i databasen vil de klare å logge inn og blir sendt til Suggestions-View
        [HttpPost("login")]
        public async Task<IActionResult> Validate(string employeeNumber, string password)
        {
            if (password == null || employeeNumber == null)
            {
                TempData["Error"] = "Venligst fyll inn alle felt!";
                return RedirectToAction("Login");
            }
            var user = userList.GetUser(employeeNumber);
            if (!(user == null)) //if user = null vil ikke neste if setning kunne fullføres derfor er denne her 
            {
                
                if (user.Password.Equals(EncryptString.Encrypt(password)))
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.EmployeeNumber));
                    claims.Add(new Claim(ClaimTypes.Name, user.Name));
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    if (user.IsAdmin)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    }
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    
                    return RedirectToAction("Index", "Suggestions");
                }
                else
                {
                    TempData["Error"] = "Error. Employee number or password is wrong";
                    return View("login");
                }
            }
            else
            {
                TempData["Error"] = "Error. Employee number or password is wrong";
                return View("login");
            }
        }

        [Authorize]  //Legger til data til koden og viser til at kun brukere som er autorisert kan akksesere denne metoden i kontrolleren 
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
            
        }
        //HttpGet tar å forcer view-et denied om en prøver å bruke sider uten tilgang til siden
        [HttpGet("Denied")]
        public IActionResult Denied()
        {
            return View();
        }
    }
}