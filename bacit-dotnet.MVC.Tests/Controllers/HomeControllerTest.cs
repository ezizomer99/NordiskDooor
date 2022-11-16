using bacit_dotnet.MVC.Controllers;
using bacit_dotnet.MVC.Models.Users;
using bacit_dotnet.MVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bacit_dotnet.MVC.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void  Test_Index_ReturnsViewResault()
        {
            //Arrange
           var mockRepo = new Mock<IUserRepository>();
            var controller = new HomeController(mockRepo.Object);
            //Act
            var result =  controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

    }
}
