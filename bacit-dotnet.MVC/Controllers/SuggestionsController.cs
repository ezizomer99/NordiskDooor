﻿using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Models.Users;
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


        [HttpGet]
        public IActionResult Edit(int? SuggestionID)
        {
            var model = new SuggestionEntity();
            
            if (SuggestionID != null)
            {
                var currentSuggestion = suggestionRepository.GetSuggestions().FirstOrDefault(x => x.SuggestionID == SuggestionID);
                if (currentSuggestion != null)
                {
                    model.SuggestionMakerID = currentSuggestion.SuggestionMakerID;
                    model.Title = currentSuggestion.Title;
                    model.Category = currentSuggestion.Category;
                    model.Team = currentSuggestion.Team;
                    model.Description = currentSuggestion.Description;
                    model.Phase = currentSuggestion.Phase;
                    model.Status = currentSuggestion.Status;
                    model.Deadline = currentSuggestion.Deadline;
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(SuggestionEntity model)
        {

            SuggestionEntity newSuggestion = new()
            {
                SuggestionMakerID = model.SuggestionMakerID,
                Title = model.Title,
                Category = model.Category,
                Team = model.Team,
                Description = model.Description,
                Phase = model.Phase,
                Status = model.Status,
                Deadline = model.Deadline,
            };
            

            return RedirectToAction("Edit");
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
