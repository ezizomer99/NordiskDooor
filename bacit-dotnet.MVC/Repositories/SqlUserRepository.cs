using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Repositories.Misc;
using bacit_dotnet.MVC.Models.Users;
using MySqlConnector;
using System.Data;

namespace bacit_dotnet.MVC.Repositories
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly ISqlConnector sqlConnector;

        public SqlUserRepository(ISqlConnector sqlConnector)
        {
            this.sqlConnector = sqlConnector;
        }
        public void Delete(string employeeNumber)
        {
            var sql = $"delete from users where employeeNumber = '{employeeNumber}'";
            var conn = sqlConnector.GetDbConnection();
            Command.RunCommand(sql, conn);
        }

        public List<UserEntity> GetUsers()
        {
            using (var connection = sqlConnector.GetDbConnection())
            {
                var reader = Command.ReadData("Select EmployeeNumber, Name, Email, Password, IsAdmin from users;", connection);
                var users = new List<UserEntity>();
                while (reader.Read())
                {
                    UserEntity user = MapUserFromReader(reader);
                    users.Add(user);
                }
                connection.Close();
                return users;

            }
        }

        public void SetAdmin(string employeeNumber, bool isAdmin)
        {
            var sql = $"update users set isAdmin={isAdmin} where employeenumber = '{employeeNumber}'";
            var conn = sqlConnector.GetDbConnection();
            Command.RunCommand(sql, conn);
        }

        private static UserEntity MapUserFromReader(IDataReader reader)
        {
            var user = new UserEntity();
            user.EmployeeNumber = reader.GetString(0);
            user.Name = reader.GetString(1);
            user.Email = reader.GetString(2);
            user.Password = reader.GetString(3);
            user.IsAdmin = reader.GetBoolean(4);
            return user;
        }

        public void Add(UserEntity user)
        {

                var sql = $"insert into users(EmployeeNumber,Name, Email, Password ) values('{user.EmployeeNumber}','{user.Name}', '{user.Email}', '{user.Password}');";
                var conn = sqlConnector.GetDbConnection();
                Command.RunCommand(sql, conn);
        }
    }
}

