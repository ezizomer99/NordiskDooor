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
        public void Test_SuggestionDelete_ReturnIndexView()
        {
            var result = _suggestionController.Delete(1) as RedirectToActionResult;

            Assert.AreEqual("Index", result.ActionName);
        }

        [TestCase(1)]
        [TestCase(2)] //Test repositoriet til suggestion har bare 2 forslag som du får ifra getSuggestions med id 1 og 2 
        public void Test_SuggestionsDetails_ReturnsView(int value)
        {
            var result = _suggestionController.Details(value) as ViewResult;

            Assert.AreEqual("Details", result.ViewName);
        }

        [TestCase(null)]
        [TestCase(4)]
        public void Test_SuggestionsDetails_RedirectsWhenSuggNotFound(int value)
        {
            var result = _suggestionController.Details(value) as RedirectToActionResult;

            Assert.AreEqual("Index", result.ActionName);
        }

        [Test]
        public void Test_SuggestionsSave_RedirectsToIndexWhenSucces()
        {
            var suggestion = new SuggestionEntity();

            var result = _suggestionController.Save(suggestion) as RedirectToActionResult;

            Assert.AreEqual("Index", result.ActionName);
        }


    }
}
