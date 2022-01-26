using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Models.Repositories.Interface
{
    public interface ITeamRepository
    {
        Team Add(Team team);
        void Update(Team team);
        void Delete(Team team);
        List<Team> GetTeams();
        Team GetTeam(int Id);




    }
}
