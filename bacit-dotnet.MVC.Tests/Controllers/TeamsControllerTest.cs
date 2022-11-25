using bacit_dotnet.MVC.Controllers;
using bacit_dotnet.MVC.Models.Teams;
using bacit_dotnet.MVC.Repositories;
using bacit_dotnet.MVC.Tests.TestRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace bacit_dotnet.MVC.Tests.Controllers
{
    [TestFixture]
    public class TeamsControllerTest
    {
        private TeamsController _teamsController;
        private ILogger<TeamsController> _logger;


        [SetUp]
        public void Setup()
        {
            var repoTeams = new TestSqlTeamRepository();
            _teamsController = new TeamsController( repoTeams,_logger);
        }

        [Test]
        public void Test_TeamsIndex_GetsAModel()
        {

            var result = _teamsController.Index() as ViewResult;

            Assert.NotNull(result.ViewData.Model);
        }
    }
}
