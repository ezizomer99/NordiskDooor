using bacit_dotnet.MVC.Models.Users;
using bacit_dotnet.MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bacit_dotnet.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new UserList();
            model.Users = userRepository.GetUsers();
            return View(model);
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
            var userList = new UserList();
            userList.Users = userRepository.GetUsers();
            if (userList.IsValidUser(userEntity))
            {
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
