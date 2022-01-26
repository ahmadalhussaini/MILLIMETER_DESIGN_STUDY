using Microsoft.EntityFrameworkCore;
using MILLIMETER_DESIGN.Models.Context;
using MILLIMETER_DESIGN.Models.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Models.Repositories.Implementation
{
    public class ProjectRepository : IProjectRepository

    {
        MILLIMETER_DESIGNContext _db;

        public ProjectRepository(MILLIMETER_DESIGNContext db)
        {
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _db = db;

        }
        public Project Add(Project entity)
        {
            var project = _db.Projects.Add(entity);
            _db.SaveChanges();
            return project.Entity;
        }

        public void Delete(Project entity)
        {
            _db.Projects.Remove(entity);
            _db.SaveChanges();
        }
        public Project GetProject(int Id)
        {
            var project = _db.Projects.SingleOrDefault(b => b.Id == Id);
            return project;
        }

        public List<Project> GetProjects()
        {
            var project = _db.Projects.ToList();
            return project;
        }
        public List<Project> GetProjects(string type)
        {
            var project = _db.Projects.Where(x=>x.Type == type).ToList();
            return project;
        }

        public void Update(Project entity)
        {
            _db.Projects.Update(entity);
            _db.SaveChanges();
        }
    }
}
