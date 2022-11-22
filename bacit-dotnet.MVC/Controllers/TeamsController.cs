using bacit_dotnet.MVC.Models.Teams;
using bacit_dotnet.MVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace bacit_dotnet.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeamsController : Controller
    {
        private readonly ITeamRepository teamRepository;
        private readonly ILogger<TeamsController> _logger;

        public TeamsController(ITeamRepository teamRepository, ILogger<TeamsController> _logger)
        {
            this.teamRepository = teamRepository;
            this._logger = _logger;
        }
        
        public IActionResult Index()
        {
            var model = new TeamList();
            model.Teams = teamRepository.GetTeams();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddTeam(TeamEntity model)
        {
            _logger.LogInformation($"{model.TeamName}");
            teamRepository.Add(model);
            return RedirectToAction("Index", "Teams");
        }

        [HttpPost]
        public IActionResult Delete(int teamId)
        {
            teamRepository.Delete(teamId);
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            var model = new TeamEntity();
            return View(model);
        }

    }

}
