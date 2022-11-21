using bacit_dotnet.MVC.Models.Users;
using bacit_dotnet.MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bacit_dotnet.MVC.Tests.TestRepositories
{
    public class TestSqlUserRepository : IUserRepository
    {
        public void Add(UserEntity user)
        {
            return;
        }
        public List<UserEntity> GetUsers()
        {
            var users = new List<UserEntity>();
            users.Add(new UserEntity { EmployeeNumber = "0000" });
            users.Add(new UserEntity { EmployeeNumber = "9999" });
            return users;
        }
        public void Delete(string email)
        {
            return;
        }
        public void SetAdmin(string employeeNumber, bool isAdmin)
        {
            return;
        }
    }
}
