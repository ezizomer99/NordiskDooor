using bacit_dotnet.MVC.DataAccess;
using System.Data;

namespace bacit_dotnet.MVC.Repositories.Misc
{
    public class Command
    {
        public static IDataReader ReadData(string query, IDbConnection connection)
        {
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            return command.ExecuteReader();
        }

        public static void RunCommand(string sql,IDbConnection conn)
        {
            using (var connection = conn)
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
