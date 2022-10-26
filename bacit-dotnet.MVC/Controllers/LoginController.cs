using bacit_dotnet.MVC.Models.Users;
using Microsoft.AspNetCore.Mvc;


namespace bacit_dotnet.MVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(UserEntity userEntity)
        {
            if (userEntity.EmployeeNumber == "1226" && userEntity.Password == "Nagasaki")
            {
                return View("LoginSuccess", userEntity);
            }
            else
            {
                return View("LoginFailure", userEntity);
            }
        }
    }
}
