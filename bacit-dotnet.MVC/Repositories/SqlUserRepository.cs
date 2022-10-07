using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Entities;
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
        public void Delete(string email)
        {
            var sql = $"delete from users where email = '{email}'";
            RunCommand(sql);
        }

        public List<UserEntity> GetUsers()
        {
            using (var connection = sqlConnector.GetDbConnection())
            {
                var reader = ReadData("Select EmployeeNumber, Name, Email, Password, isAdmin from users;", connection);
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

        public void Save(UserEntity user)
        {
            UserEntity existingUser = null;
            using (var connection = sqlConnector.GetDbConnection())
            {
                var reader = ReadData("Select EmployeeNumber, Name, Email, Password, IsAdmin from users;", connection);
               
                while (reader.Read())
                {
                    existingUser = MapUserFromReader(reader);
                }
                connection.Close();
            }
            if (existingUser!=null)
            {
                var sql = $@"update users 
                                set 

                                Name = '{user.Name}', 
                                Password='{user.Password}',
                                where email = '{user.Email}';";
                RunCommand(sql);
            }
            else 
            {
                var sql = $"insert into users(Name, Email, Password) values('{user.Name}', '{user.Email}', '{user.Password}');";
                RunCommand(sql);
            }
            
        }

        private void RunCommand(string sql)
        {
            using (var connection = sqlConnector.GetDbConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private IDataReader ReadData(string query, IDbConnection connection)
        {
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            return command.ExecuteReader();
        }
    }
}

