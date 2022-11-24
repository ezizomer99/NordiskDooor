using bacit_dotnet.MVC.Models.Users;
using bacit_dotnet.MVC.Repositories;
using bacit_dotnet.MVC.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bacit_dotnet.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private UserList userList = new UserList();
       

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            userList.Users = userRepository.GetUsers();
        }
        
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            
            return View(userList);
        }

        [HttpPost]
        public IActionResult AddUser(UserEntity model)
        {
            
            model.Password = EncryptString.Encrypt(model.Password);
            model.RepeatPassword = EncryptString.Encrypt(model.RepeatPassword);
            if (!model.Password.Equals(model.RepeatPassword))
            {
                string error = "Passordfeltene er ikke like";
                TempData["Error"] = error;
                return RedirectToAction("Register");
            }
            userRepository.Add(model);
            return RedirectToAction("Index","suggestions");
        }

        [HttpPost]
        public IActionResult Delete(string employeeNumber)
        {
            userRepository.Delete(employeeNumber);
            return RedirectToAction("Index");
        }
        
        public IActionResult Register()
        {
            var model = new UserEntity();
            return View(model);
        }

        public IActionResult ProcessLogin(UserEntity userEntity)
        {
            
            if (userList.IsValidUser(userEntity))
            {
                userEntity = userList.GetUser(userEntity.EmployeeNumber);
                return View("LoginSuccess", userEntity);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult setAdmin(string employeeNumber, bool isAdmin)
        {
            userRepository.SetAdmin(employeeNumber, isAdmin);
            return RedirectToAction("Index");
        }
        public IActionResult Login()
        {
            return View();
        }

    }
}
