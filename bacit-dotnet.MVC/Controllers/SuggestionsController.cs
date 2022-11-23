using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Models.Teams;
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
        private readonly ICategoryRepository categoryRepository;


        public SuggestionsController(ISuggestionRepository suggestionRepository, ITeamRepository teamRepository, ICategoryRepository categoryRepository)
        {
            this.suggestionRepository = suggestionRepository;
            this.teamRepository = teamRepository;
            this.categoryRepository = categoryRepository;
        }
        [HttpGet]
        public IActionResult Index(string searchWord)
        {
            var teamlist = teamRepository.GetTeams();
            var model = new SuggestionList();
            ViewData["TeamList"] = teamlist;
            if (searchWord == null || searchWord.Equals("all"))
            {
                model.Suggestions = suggestionRepository.GetSuggestions();
                return View(model);
            } else
            {
                model.Suggestions = suggestionRepository.GetSuggestionsWithSearchQyery(searchWord);
                return View(model);
            }
        }
        public IActionResult Create()
        {
            var model = new SuggestionEntity();
            model.teamList = teamRepository.GetTeams();
            var categoires = categoryRepository.GetCategories();
            ViewData["Category"] = categoires;
            return View(model);
        }



        [HttpGet]
        public IActionResult Edit(int? SuggestionID)
        {
            var suggestion = suggestionRepository.GetSuggestions().FirstOrDefault(x => x.SuggestionID == SuggestionID);
            suggestion.teamList = teamRepository.GetTeams();
            var categoires = categoryRepository.GetCategories();
            ViewData["Category"] = categoires;
            return View(suggestion);
        }

        [HttpPost]
        public IActionResult Save(SuggestionEntity model)
        {
            suggestionRepository.Edit(model);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int SuggestionID)
        {
            suggestionRepository.Delete(SuggestionID);
            return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }

            var suggestion = suggestionRepository.GetSuggestions()
                .FirstOrDefault(m => m.SuggestionID == id); //returns the first value of multiple elemnts that meets the requirements
            return View("Details",suggestion);
        }
    }
}
