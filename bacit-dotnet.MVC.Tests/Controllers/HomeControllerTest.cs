using bacit_dotnet.MVC.Controllers;
using bacit_dotnet.MVC.Tests.TestRepositories;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace bacit_dotnet.MVC.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Test_Denied_ReturnView()
        {
            var userRepo = new TestSqlUserRepository();
            var controller = new HomeController(userRepo);
            

            var result = controller.Denied() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void Test_LogOut_ReturnNotNull()
        {
            var userRepo = new TestSqlUserRepository();
            var controller = new HomeController(userRepo);


            var result = controller.Logout();

            Assert.IsNotNull(result);
        }
    }
}
