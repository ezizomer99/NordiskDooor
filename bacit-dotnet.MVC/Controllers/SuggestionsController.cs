 using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bacit_dotnet.MVC.Controllers
{
    [Authorize]
    public class SuggestionsController : Controller
    {
        private readonly ISuggestionRepository suggestionRepository;
        private readonly ITeamRepository teamRepository;

        public SuggestionsController(ISuggestionRepository suggestionRepository, ITeamRepository teamRepository)
        {
            this.suggestionRepository = suggestionRepository;
            this.teamRepository = teamRepository;
        }
        
        public IActionResult Index()
        {
            var model = new SuggestionList();
            model.Suggestions = suggestionRepository.GetSuggestions();
            return View(model);
        }
        public IActionResult Create()
        {
            var model = new SuggestionEntity();
            model.teamList = teamRepository.GetTeams();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int SuggestionID)
        {
            suggestionRepository.Delete(SuggestionID);
            return RedirectToAction("index");
        }
        [HttpPost]
        public IActionResult AddSuggestion(SuggestionEntity model)
        {
            model.SuggestionMakerID = User.Identity.GetUserId();
            suggestionRepository.AddSuggestion(model);
            return RedirectToAction("Index", "Suggestions"); 
        }
        
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestion = suggestionRepository.GetSuggestions()
                .FirstOrDefault(m => m.SuggestionID == id); //returns the first value of multiple elemnts that meets the requirements
            if (suggestion == null)
            {
                return NotFound();
            }

            return View(suggestion);
        }
    }
}
