using bacit_dotnet.MVC.Repositories;

namespace bacit_dotnet.MVC.Models.Users
{
    public class UserList
    {
        public List<UserEntity> Users { get; set; }

        public bool IsValidUser(UserEntity user)
        {
            return Users.Any(m => m.EmployeeNumber == user.EmployeeNumber && m.Password == user.Password);
        }

        public UserEntity GetUser(string employeeNumber) //Vill oppstå problemer hvis Name er brukt istedenfor EmployeeNumber for å logge inn
        {
            foreach (UserEntity user in Users)
            {
                if (user.EmployeeNumber.Equals(employeeNumber)){
                    return user;
                }

            }
            return null;
        }
    }
}
