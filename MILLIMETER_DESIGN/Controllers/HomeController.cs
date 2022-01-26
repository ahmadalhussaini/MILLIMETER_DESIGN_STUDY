using Microsoft.AspNetCore.Mvc;
using MILLIMETER_DESIGN.Models.Repositories.Interface;
using MILLIMETER_DESIGN.ViweModel.Home;
using MILLIMETER_DESIGN.ViweModel.Image;
using MILLIMETER_DESIGN.ViweModel.Project;
using MILLIMETER_DESIGN.ViweModel.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private IImageRepository _imageRepository;
        private ITeamRepository _teamRepository;

        public HomeController(IProjectRepository projectRepository, IImageRepository imageRepository,ITeamRepository teamRepository)
        {
            _projectRepository = projectRepository;
            _imageRepository = imageRepository;
            _teamRepository = teamRepository;
        }
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Projects(string type)
        { 
            var projects = _projectRepository.GetProjects(type).OrderByDescending(x=>x.Id).ToList();
            var model = new List<HomeViewModel>();
            var vm = new HomeListViewModel();
            foreach (var elem in projects) {
                var imge = _imageRepository.GetImages(elem.Id).Take(1).ToList();
               
                model.Add(new HomeViewModel()
                {
                    Id = elem.Id,
                    Name_En = elem.Name_En,
                    FirstImage = imge[0].PathImg,
                    Type=elem.Type
                
                });
                
            }
            vm.Projects = model;
            
            return View(vm);
        }
        public IActionResult Project(int id)
        {
           
            var project = _projectRepository.GetProject(id);
            var images = _imageRepository.GetImages(project.Id);
            var imagesvm = new List<ImageViewModel>();
            foreach (var elem in images) {

                imagesvm.Add(new ImageViewModel() { 
                PathImg = elem.PathImg                
                });
            }
            var projectView = new ProjectViewModel()
            {
               
               Name_En =project.Name_En,
               Name_Ar=project.Name_Ar,
               Description_En=project.Description_En,
               Description_Ar=project.Description_Ar,
               Place_En=project.Place_En,
               Place_Ar=project.Place_Ar,
               CreatedAt=project.CreatedAt,
               images = imagesvm

            };

          //try جرب شغله

            return View(projectView);
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Protfolio()
        {
            return View();
        }
        public IActionResult Teams()
        {
            var teams = _teamRepository.GetTeams();
            var model = new List<TeamViewModel>();
            var vm = new TeamListViewModel();
            foreach (var elem in teams)
            {

                model.Add(new TeamViewModel()
                {
                    Id = elem.Id,
                    PathImg=elem.PathImg,
                    Name=elem.Name,
                    Position=elem.Position,
                    Description=elem.Description,
                    Country=elem.Country
                   

                });

            }
            vm.TeamViewModel = model;

            return View(vm);
        }
        public IActionResult ContactUs()
        {
            return View();
        }

    }
}
