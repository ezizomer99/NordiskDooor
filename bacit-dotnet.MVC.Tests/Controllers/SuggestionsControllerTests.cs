using bacit_dotnet.MVC.Controllers;
using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Tests.TestRepositories;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;


namespace bacit_dotnet.MVC.Tests.Controllers
{
    [TestFixture]
    public class SuggestionsControllerTests
    {
        private SuggestionsController _suggestionController;
        
        [SetUp]
        public void Setup()
        {
            var repoSuggestions = new TestSqlSuggestionsRepository();
            var repoTeams = new TestSqlTeamRepository();
            var repoCategory = new TestSqlCategoryRepository();
            _suggestionController = new SuggestionsController(repoSuggestions,repoTeams, repoCategory);
        }

        [Test]
        public void Test_SuggestionDetails_ReturnView()
        {
            var result = _suggestionController.Details(1) as ViewResult;

            Assert.AreEqual("Details", result.ViewName);
        }

       [Test]
        public void Test_AddSuggestion_WhenTitleIsNull()
        {
            //Tempdata vil føre til en NullRefrenceException, uten TempData vil denne fungere
            //Har noe med sesssion å gjøre
            var repoSuggestions = new TestSqlSuggestionsRepository();
            var repoTeams = new TestSqlTeamRepository();
            var repoCategory = new TestSqlCategoryRepository();
            var testController = new SuggestionsController(repoSuggestions, repoTeams, repoCategory);
            var suggestion = new SuggestionEntity
            {
                SuggestionID = 1,
                SuggestionMakerID = "0000",
                Title = null,
                Description = "DawdAW"
            };

            var result = testController.AddSuggestion(suggestion) as RedirectToActionResult;

            Assert.AreEqual("Create", result.ActionName);
        }

        [Test]
        public void Test_SuggestionDelete_ReturnIndexView()
        {
            var result = _suggestionController.Delete(1) as RedirectToActionResult;

            Assert.AreEqual("Index", result.ActionName);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void Test_SuggestionsDetails_ReturnsSuggestion(int value)
        {
            var result = _suggestionController.Details(value);
            
        }

    }
}
