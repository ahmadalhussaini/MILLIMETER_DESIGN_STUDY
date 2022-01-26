using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Models.Repositories.Interface
{
   public interface IProjectRepository
    {
        List<Project> GetProjects();
        List<Project> GetProjects(string type);
        Project GetProject(int Id);
        Project Add(Project product);
        void Update(Project project);
        void Delete(Project project);
  }
}
