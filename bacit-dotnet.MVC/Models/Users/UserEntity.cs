

namespace bacit_dotnet.MVC.Models.Users
{
    public class UserEntity
    {
        public string EmployeeNumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
