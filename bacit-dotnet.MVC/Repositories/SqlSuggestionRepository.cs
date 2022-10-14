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
                var reader = ReadData("Select * from suggestions",connection);
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
            var suggestion = new SuggestionEntity();
            suggestion.SuggestionID = reader.GetInt32(0);
            suggestion.SuggestionMakerID = reader.GetString(1);
            suggestion.Title = reader.GetString(2);
            suggestion.Category = reader.GetString(3);
            suggestion.Description = reader.GetString(4);
            suggestion.Phase = reader.GetString(5);
            suggestion.Status = reader.GetString(6);
            suggestion.TimeStamp = reader.GetString(7);
            suggestion.Deadline = reader.GetString(8);
            return suggestion;
        }

        public void AddSuggestion(SuggestionEntity suggestion)
        {
            //SuggestionEntity existingSuggestion = GetSuggestions(suggestion.)

            var sql = $"insert into suggestions(SuggestionMakerID, Title, Category, TeamID, Description, Phase, Status, Deadline) values('{suggestion.SuggestionMakerID}', '{suggestion.Title}', '{suggestion.Category}', '{suggestion.Team}', '{suggestion.Description}', '{suggestion.Phase}', '{suggestion.Status}', '{suggestion.Deadline}');";
            RunCommand(sql);
        }
        public void Delete(string email)
        {
            
           Console.WriteLine("Work In Progress");
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

