using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Models.Teams;
using MySqlConnector;
using System.Data;

namespace bacit_dotnet.MVC.Repositories
{
    public class SqlTeamRepository : ITeamRepository
    {
        private readonly ISqlConnector sqlConnector;

        public SqlTeamRepository(ISqlConnector sqlConnector)
        {
            this.sqlConnector = sqlConnector;
        }
        public void Delete(int teamID)
        {
            var sql = $"delete from teams where TeamID = '{teamID}';";
            RunCommand(sql);
        }

        public List<TeamEntity> GetTeams()
        {
            using (var connection = sqlConnector.GetDbConnection())
            {
                var reader = ReadData("Select TeamID, TeamName from teams;", connection);
                var teams = new List<TeamEntity>();
                while (reader.Read())
                {
                    TeamEntity team = MapTeamFromReader(reader);
                    teams.Add(team);
                }
                connection.Close();
                return teams;

            }
        }

        private static TeamEntity MapTeamFromReader(IDataReader reader)
        {
            var team = new TeamEntity();
            team.TeamID = reader.GetInt16(0);
            team.TeamName = reader.GetString(1);
            return team;
        }

        public void Add(TeamEntity team)
        {

            var sql = $"insert into teams(TeamID,TeamName) values('{team.TeamID}','{team.TeamName}');";
            RunCommand(sql);
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
