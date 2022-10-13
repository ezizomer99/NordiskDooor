using bacit_dotnet.MVC.Entities;
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
        public IActionResult Index(string? email)
        {
            var model = new UserViewModel();
            model.Users = userRepository.GetUsers();
            if (email != null)
            {
                var currentUser = model.Users.FirstOrDefault(x => x.Email == email);
                if (currentUser != null)
                {
                    model.EmployeeNumber = currentUser.EmployeeNumber;
                    model.Email = currentUser.Email;
                    model.Name = currentUser.Name;
                    model.IsAdmin = currentUser.IsAdmin;
                    model.Password = currentUser.Password;
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult AddUser(UserViewModel model)
        {

            UserEntity newUser = new UserEntity
            {
                EmployeeNumber = model.EmployeeNumber,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
            };
            userRepository.Add(newUser);
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
            var model = new UserViewModel();
            return View(model);           
        }

    }
}
