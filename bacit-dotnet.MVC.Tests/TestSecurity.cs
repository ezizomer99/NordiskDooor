using bacit_dotnet.MVC.Security;
using NUnit.Framework;

namespace bacit_dotnet.MVC.Tests
{
    [TestFixture]
    public class TestSecurity
    {
        [Test]
        public void Test_Password_Hash()
        {
            //Arrange
            var expected = "aa71f196522c6da5ca2155b328d3cb4db51458134ee62bb37fd872a529f655be";
            //Act
            var result = EncryptString.Encrypt("Cookies123");
            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
