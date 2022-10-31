 using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bacit_dotnet.MVC.Controllers
{
    [Authorize]
    public class SuggestionsController : Controller
    {
        private readonly ISuggestionRepository suggestionRepository;

        public SuggestionsController(ISuggestionRepository suggestionRepository)
        {
            this.suggestionRepository = suggestionRepository;
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
            return View(model);
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
