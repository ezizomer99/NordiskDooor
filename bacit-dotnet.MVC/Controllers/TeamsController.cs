using bacit_dotnet.MVC.Models.Teams;
using bacit_dotnet.MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bacit_dotnet.MVC.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamRepository teamRepository;

        public TeamsController(ITeamRepository teamRepository)
        {
            this.teamRepository = teamRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new TeamList();
            model.Teams = teamRepository.GetTeams();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddTeam(TeamEntity model)
        {
            teamRepository.Add(model);
            return RedirectToAction("Index", "Teams");
        }

        [HttpPost]
        public IActionResult Delete(string TeamID)
        {
            teamRepository.Delete(TeamID);
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            var model = new TeamEntity();
            return View(model);
        }

    }

}
