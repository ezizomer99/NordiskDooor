using bacit_dotnet.MVC.Entities;
using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bacit_dotnet.MVC.Controllers
{
    public class SuggestionsController : Controller
    {
        private readonly ISuggestionRepository suggestionRepository;

        public SuggestionsController(ISuggestionRepository suggestionRepository)
        {
            this.suggestionRepository = suggestionRepository;
        }
        [HttpGet]
        public IActionResult Index(string? SuggestionID)
        {
            var model = new SuggestionViewModel();
            model.Suggestions = suggestionRepository.GetSuggestions();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddSuggestion(SuggestionViewModel model)
        {

            SuggestionEntity newSuggestion = new SuggestionEntity
            {
                SuggestionID = model.SuggestionID,
                SuggestionMakerID = model.SuggestionMakerID,
                Title = model.Title,
                Category = model.Category,
                Team = model.Team,
                Description = model.Description,
                Phase = model.Phase,
                Status = model.Status,
                TimeStamp = model.TimeStamp,
                Deadline = model.Deadline,
            };
            suggestionRepository.AddSuggestion(newSuggestion);
            return RedirectToAction("Index", "Suggestions");
        }
    }
}
