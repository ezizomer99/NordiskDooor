using bacit_dotnet.MVC.Models.Teams;

namespace bacit_dotnet.MVC.Repositories
{
    public interface ITeamRepository
    {
        void Add(TeamEntity team);
        List<TeamEntity> GetTeams();
        void Delete(string TeamID);
    }
}
