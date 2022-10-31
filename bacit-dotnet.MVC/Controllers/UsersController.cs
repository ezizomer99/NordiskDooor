using bacit_dotnet.MVC.Models.Users;
using bacit_dotnet.MVC.Repositories;
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
        [HttpGet]
        public IActionResult Index()
        {
            
            return View(userList);
        }

        [HttpPost]
        public IActionResult AddUser(UserEntity model)
        {
            userRepository.Add(model);
            return RedirectToAction("Index","Users");
        }

        [HttpPost]
        public IActionResult Delete(string email)
        {
            userRepository.Delete(email);
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

        public IActionResult Login()
        {
            return View();
        }

    }
}
