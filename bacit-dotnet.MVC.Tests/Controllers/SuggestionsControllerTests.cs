using bacit_dotnet.MVC.Controllers;
using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bacit_dotnet.MVC.Tests.Controllers
{
    public class SuggestionsControllerTests
    {

        [Fact]
        public void SuggestionIndex_Returns_View()
        {
            //Arrange
            var mockRepoSugg = new Mock<ISuggestionRepository>();
            var mockRepoTeam =  new Mock<ITeamRepository>();
            var controller = new SuggestionsController(mockRepoSugg.Object, mockRepoTeam.Object);
            
            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_SuggestionCreate_ReturnsView()
        {
            //Arrange
            var mockRepoSugg = new Mock<ISuggestionRepository>();
            var mockRepoTeam = new Mock<ITeamRepository>();
            var controller = new SuggestionsController(mockRepoSugg.Object, mockRepoTeam.Object);

            //Act
            var result = controller.Create();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

    }
}
