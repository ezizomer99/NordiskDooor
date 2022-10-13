using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Entities;
using MySqlConnector;
using System.Data;

namespace bacit_dotnet.MVC.Repositories
{
    public class SqlSuggestionRepository : ISuggestionRepository
    {

        private readonly ISqlConnector sqlConnector;

        public SqlSuggestionRepository(ISqlConnector sqlConnector)
        {
            this.sqlConnector = sqlConnector;
        }

        public List<SuggestionEntity> GetSuggestions()
        {
            using (var connection = sqlConnector.GetDbConnection())
            {
                var reader = ReadData("Select",connection);
                var suggestions = new List<SuggestionEntity>();
                while (reader.Read())
                {
                    SuggestionEntity suggestion = MapUSuggestionFromReader(reader);
                    suggestions.Add(suggestion);
                }
                connection.Close();
                return suggestions;

            }
        }

        private static SuggestionEntity MapUSuggestionFromReader(IDataReader reader)
        {
            var suggestions = new SuggestionEntity();
            suggestions.Title = reader.GetString(0);
            return suggestions;
        }

        public void Add(SuggestionEntity suggestion)
        {
            SuggestionEntity existingSuggestion = GetSuggestions(suggestion.)
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

