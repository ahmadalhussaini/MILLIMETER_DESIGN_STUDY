using Microsoft.EntityFrameworkCore;
using MILLIMETER_DESIGN.Models.Context;
using MILLIMETER_DESIGN.Models.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Models.Repositories.Implementation
{
    public class TeamRepository : ITeamRepository
    {
        MILLIMETER_DESIGNContext _db;

        public TeamRepository(MILLIMETER_DESIGNContext db)
        {
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _db = db;

        }
        public Team Add(Team entity)
        {
            var team = _db.Teams.Add(entity);
            _db.SaveChanges();
            return team.Entity;
        }

        public void Delete(Team entity)
        {
            _db.Teams.Remove(entity);
            _db.SaveChanges();
        }

        public Team GetTeam(int Id)
        {
            var team = _db.Teams.SingleOrDefault(b => b.Id == Id);
            return team;
        }

        public List<Team> GetTeams()
        {
            var team = _db.Teams.ToList();
            return team;
        }

        public void Update(Team entity)
        {
            _db.Teams.Update(entity);
            _db.SaveChanges();
        }
    }
}
