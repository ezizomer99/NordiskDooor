using bacit_dotnet.MVC.Entities;
using bacit_dotnet.MVC.Repositories;

namespace bacit_dotnet.MVC.Models.Users
{
    public class UserViewModel
    {
        
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsAdmin { get; set; }
        public string? ValidadtionErrorMessage { get; set; } 
        public List<UserEntity>? Users { get; set; }
    }
}
