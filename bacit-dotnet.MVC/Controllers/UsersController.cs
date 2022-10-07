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
                  
                    model.Name = currentUser.Name;
                    model.Email = currentUser.Email;
                    model.Password = currentUser.Password;
                    model.IsAdmin = currentUser.IsAdmin;
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Save(UserViewModel model)
        {

            UserEntity newUser = new UserEntity
            {
                
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                IsAdmin = model.IsAdmin,
            };
            userRepository.Save(newUser);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string email)
        {
            userRepository.Delete(email);
            return RedirectToAction("Index");
        }
    }
}
