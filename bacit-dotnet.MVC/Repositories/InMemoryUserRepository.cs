using bacit_dotnet.MVC.Entities;

namespace bacit_dotnet.MVC.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private List<UserEntity> users;
        public InMemoryUserRepository()
        {
            users = new List<UserEntity>();
        }
        public void Save(UserEntity user)
        {
            var existingUser = GetUserByEmail(user.Email);
            if (existingUser == null)
            {
                users.Add(user);
            }
            else
            {
                existingUser.EmployeeNumber = user.EmployeeNumber;
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.IsAdmin = user.IsAdmin;
            }
        }

        public List<UserEntity> GetUsers()
        {
            return users;
        }

        public void Delete(string email)
        {
            UserEntity? foundUser = GetUserByEmail(email);
            if (foundUser != null)
            {
                users.Remove(foundUser);
            }
        }

        private UserEntity? GetUserByEmail(string email)
        {
            return users
                             .FirstOrDefault(user =>
                             user.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
