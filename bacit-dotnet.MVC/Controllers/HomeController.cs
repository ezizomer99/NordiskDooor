using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Models;
using bacit_dotnet.MVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace bacit_dotnet.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository userRepository;
        private readonly ITeamRepository teamRepository;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository, ITeamRepository teamRepository)
        {
            _logger = logger;
            this.userRepository = userRepository;
            this.teamRepository = teamRepository;
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
    }
}