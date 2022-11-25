using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Models.Suggestions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MySqlConnector;
using System.Data;
using bacit_dotnet.MVC.Repositories.Misc;

namespace bacit_dotnet.MVC.Repositories
{
    public class SqlSuggestionRepository : ISuggestionRepository
    {

        private readonly SqlConnector sqlConnector;

        public SqlSuggestionRepository(SqlConnector sqlConnector)
        {
            this.sqlConnector = sqlConnector;
        }
        //GetSuggestions gir sql query til readData og gir det den får til MapSuggestionFromReader og får tilbake enteties som blir lagt inn i i listen suggestions
        public List<SuggestionEntity> GetSuggestions()
        {
            using (var connection = sqlConnector.GetDbConnection())
            {
                var reader = Command.ReadData("select suggestionid, name, title,description,categoryname, teamname,phase,status,timestamp,deadline  from ((suggestions inner join users on if(suggestionmakerid is null, '9999',suggestionmakerid)=employeenumber) inner join category on suggestions.categoryid = category.categoryid) inner join teams on suggestions.teamid=teams.teamid;", connection);
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
        public List<SuggestionEntity> GetSuggestionsWithSearchQyery(string searchWord)
        {
            using (var connection = sqlConnector.GetDbConnection())
            {
                var reader = Command.ReadData($"select suggestionid, name, title,description,categoryname, teamname,phase,status,timestamp,deadline  from ((suggestions inner join users on if(suggestionmakerid is null, '9999',suggestionmakerid)=employeenumber) inner join category on suggestions.categoryid = category.categoryid) inner join teams on suggestions.teamid=teams.teamid and teamname='{searchWord}';", connection);
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
            var conn = sqlConnector.GetDbConnection();
            Command.RunCommand(sql,conn);
        }
        public void Delete(int SuggestionID)
        {
            var sql = $" delete from suggestions where SuggestionID = '{SuggestionID}'";
            var conn = sqlConnector.GetDbConnection();
            Command.RunCommand(sql, conn);
        }

        public void Edit(SuggestionEntity suggestion)
        {

            if (suggestion == null)
            {
                throw new Exception("Suggestion does not exist");
            }
            var sql = $@"update suggestions 
                                set 
                                   Title = '{suggestion.Title}', 
                                   Categoryid='{suggestion.Category}',
                                   Teamid = '{suggestion.Team}',
                                   Description ='{suggestion.Description}', 
                                   Phase ='{suggestion.Phase}' ,
                                   Status ='{suggestion.Status}' ,
                                   Deadline ='{suggestion.Deadline}' 
                                where suggestionID = '{suggestion.SuggestionID}';";
            var conn = sqlConnector.GetDbConnection();
            Command.RunCommand(sql, conn);
        }
    }
}

