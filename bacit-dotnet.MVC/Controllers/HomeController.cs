
//Forteller kompilatoren å bruke ulike namespaces så en ikke trenger å skrive det fulle navnet av klassen når en skal bruke den
//Gjør kode til mær clean kode og gjør prosjektet enklere å kode med
using bacit_dotnet.MVC.Models.Users;
using bacit_dotnet.MVC.Repositories;
using bacit_dotnet.MVC.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

//namespace opprettes hvor visse funksjoner, variabler, etc beskrives
namespace bacit_dotnet.MVC.Controllers
{
    //HomeController som arver fra Controller klassen som støtter for MVC tilegg
    public class HomeController : Controller
    {
        //Leser fra interface som har metoden Getusers() som henter fra databasen
        private readonly IUserRepository userRepository;
        private UserList userList = new UserList();
        
        //Constructoren til klassen
        // tar inn et objekt av IUserRepository setter variablen til userRepository
        // kaller på GetUsers() metoden på objektet  
        public HomeController( IUserRepository userRepository)
        {
            
            this.userRepository = userRepository;
            userList.Users = userRepository.GetUsers();
        }

        //Metode for å returnere view-et, er IActionResult som kan returnere View eller RedirectToAction 
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
        //HttpPost ber requesten om serveren godtar dataene, i dette tilfellet om en bruker er validert eller ikke
        //Hvis EmployeeNumber og Password er korrekt med det som er lagret i databasen vil de klare å logge inn og blir sendt til Suggestions-View
        //Async gjør at det er mulig å vente for signin (Task er "waitable")
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
        //Legger til data til koden og viser til at kun brukere som er autorisert kan akksesere denne metoden i kontrolleren 
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
            
        }
        //HttpGet tar å forcer view-et denied om en prøver å bruke ulike sider uten tilgang
        [HttpGet("Denied")]
        public IActionResult Denied()
        {
            return View();
        }
    }
}