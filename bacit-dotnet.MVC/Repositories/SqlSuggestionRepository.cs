using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Models.Suggestions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        //GetSuggestions gir sql query til readData og gir det den får til MapSuggestionFromReader og får tilbake enteties som blir lagt inn i i listen suggestions
        public List<SuggestionEntity> GetSuggestions()
        {
            using (var connection = sqlConnector.GetDbConnection())
            {
                var reader = ReadData("select suggestionid, name, title,description,categoryname, teamname,phase,status,timestamp,deadline  from ((suggestions inner join users on if(suggestionmakerid is null, '9999',suggestionmakerid)=employeenumber) inner join category on suggestions.categoryid = category.categoryid) inner join teams on suggestions.teamid=teams.teamid;", connection);
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
        //MapSuggestionFromReader tar og leser hver enkelt rad i tabelen suggestions og lager enteties ut ifra dem
        private static SuggestionEntity MapUSuggestionFromReader(IDataReader reader)
        {
            var suggestion = new SuggestionEntity();
            suggestion.SuggestionID = reader.GetInt32(0);
            suggestion.SuggestionMakerID = reader.GetString(1);
            suggestion.Title = reader.GetString(2);
            suggestion.Description = reader.GetString(3);
            suggestion.Category = reader.GetString(4);
            suggestion.Team = reader.GetString(5);
            suggestion.Phase = reader.GetString(6);
            suggestion.Status = reader.GetString(7);
            suggestion.TimeStamp = reader.GetDateTime(8);
            suggestion.Deadline = reader.GetString(9);
            return suggestion;
        }

        public void AddSuggestion(SuggestionEntity suggestion)
        {
            var sql = $"insert into suggestions(SuggestionMakerID, Title, CategoryID, TeamID, Description, Phase, Status, Deadline) values('{suggestion.SuggestionMakerID}', '{suggestion.Title}', '{suggestion.Category}', '{suggestion.Team}', '{suggestion.Description}', '{suggestion.Phase}', '{suggestion.Status}', '{suggestion.Deadline}');";
            RunCommand(sql);
        }
        public void Delete(int SuggestionID)
        {
            var sql = $" delete from suggestions where SuggestionID = '{SuggestionID}'";
            RunCommand(sql);
        }
        //runcommand får en string og sender stringen til databasen 
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
        //readData leser 
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

