using bacit_dotnet.MVC.Models.Users;

namespace bacit_dotnet.MVC.Repositories
{
    public interface IUserRepository
    {
        void Add(UserEntity user);
        List<UserEntity> GetUsers();
        void Delete(string email);

        void SetAdmin(string employeeNumber, bool isAdmin);
    }
}