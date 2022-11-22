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
        
        [SetUp]
        public void Setup()
        {
            var repoSuggestions = new TestSqlSuggestionsRepository();
            var repoTeams = new TestSqlTeamRepository();
            var repoCategory = new TestSqlCategoryRepository();
            var testController = new SuggestionsController(repoSuggestions,repoTeams, repoCategory);
        }

        [Test]
        public void Test_SuggestionIndex_ReturnView()
        {
            var repoSuggestions = new TestSqlSuggestionsRepository();
            var repoTeams = new TestSqlTeamRepository();
            var repoCategory = new TestSqlCategoryRepository();
            var testController = new SuggestionsController(repoSuggestions, repoTeams, repoCategory);

            var result = testController.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void Test_Suggestiondetails_ReturnView()
        {
            var repoSuggestions = new TestSqlSuggestionsRepository();
            var repoTeams = new TestSqlTeamRepository();
            var repoCategory = new TestSqlCategoryRepository();
            var testController = new SuggestionsController(repoSuggestions, repoTeams, repoCategory);

            var result = testController.Details(1) as ViewResult;

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
           
            var repoSuggestions = new TestSqlSuggestionsRepository();
            var repoTeams = new TestSqlTeamRepository();
            var repoCategory = new TestSqlCategoryRepository();
            var testController = new SuggestionsController(repoSuggestions, repoTeams, repoCategory);


            var result = testController.Delete(1) as RedirectToActionResult;

            Assert.AreEqual("Index", result.ActionName);
        }

    }
}
