using bacit_dotnet.MVC.Models.Teams;
using bacit_dotnet.MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bacit_dotnet.MVC.Tests.TestRepositories
{
    internal class TestSqlTeamRepository : ITeamRepository
    {
        public void Add(TeamEntity team)
        {
            return;
        }
        public List<TeamEntity> GetTeams()
        {
            return new List<TeamEntity>();
        }
        public void Delete(int TeamID)
        {
            return;
        }
    }
}
