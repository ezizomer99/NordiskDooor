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
       

        [HttpPost]
        public ActionResult Edit(int Suggestion)
        {

            suggestionRepository.Edit(Suggestion);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int SuggestionID)
        {
            suggestionRepository.Delete(SuggestionID);
            return RedirectToAction("index");
        }
        [HttpPost]
        public IActionResult AddSuggestion(SuggestionEntity model)
        {
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
