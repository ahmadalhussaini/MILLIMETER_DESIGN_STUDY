using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MILLIMETER_DESIGN.Models;
using MILLIMETER_DESIGN.Models.Repositories.Implementation;
using MILLIMETER_DESIGN.Models.Repositories.Interface;
using MILLIMETER_DESIGN.ViweModel;
using MILLIMETER_DESIGN.ViweModel.Image;
using MILLIMETER_DESIGN.ViweModel.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Controllers
{
   [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private IImageRepository _imageRepository;
        public ProjectController(IProjectRepository projectRepository, IImageRepository imageRepository)
        {
            _projectRepository = projectRepository;
            _imageRepository = imageRepository;
        }

        // GET: ProjectController
      
        public ActionResult Index()
        {
            var projects = _projectRepository.GetProjects();
            var List = new ProjectListViewModel();
            var _projects = new List<ProjectViewModel>();
            foreach (var elem in projects)
            {
                var model = new ProjectViewModel
                {
                    Id = elem.Id,
                    Name_En = elem.Name_En,
                    Name_Ar = elem.Name_Ar,
                    Description_En = elem.Description_En,
                    Description_Ar = elem.Description_Ar,
                    Place_En = elem.Place_En,
                    Place_Ar = elem.Place_Ar,
                    Type = elem.Type,
                    CreatedAt = elem.CreatedAt
               
                };

                _projects.Add(model);

            }
            List.ProjectsViewModel = _projects;
            return View(List);
        }

        // GET: ProjectController/Details/5
        public ActionResult Details(int id)
        {
            var project = _projectRepository.GetProject(id);
            var imges = _imageRepository.GetImages(id);
            var List = new ImageListViewModel();
            var _images = new List<ImageViewModel>();
            foreach (var elem in imges)
            {
                var mode = new ImageViewModel
                {
                    PathImg = elem.PathImg
                };
                _images.Add(mode);
            }
            var model = new ProjectViewModel
            {
                Id = project.Id,
                Name_En = project.Name_En,
                Name_Ar = project.Name_Ar,
                Description_En = project.Description_En,
                Description_Ar = project.Description_Ar,
                Place_En = project.Place_En,
                Place_Ar = project.Place_Ar,
                Type = project.Type,
                CreatedAt = project.CreatedAt,
                images = _images

            };
          
            return View(model);
            }

        // GET: ProjectController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectViewModel project)
        {
            List<string> paths = new List<string>();
            foreach (var elem in project._images)
            {
                var path = Upload(elem, "porjects");
                paths.Add(path);
            }
            var _project = new Project
            {
                Name_En=project.Name_En,
                Name_Ar=project.Name_Ar,
                Description_En=project.Description_En,
                Description_Ar=project.Description_Ar,
                Place_En=project.Place_En,
                Place_Ar=project.Place_Ar,
                Type = project.Type,
                CreatedAt=project.CreatedAt        
            };
           var projectid = _projectRepository.Add(_project);
            foreach (var elem in paths)
            {
                var image = new Image
                {
                    PathImg = elem,
                    ProjectId= projectid.Id,
                    CreatedAt= projectid.CreatedAt
                };
                _imageRepository.Add(image);
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: ProjectController/Edit/5
        public ActionResult Edit(int id)
        {
            var project = _projectRepository.GetProject(id);
            var _project = new ProjectViewModel
            {
                Id = project.Id,
                Name_En = project.Name_En,
                Name_Ar = project.Name_Ar,
                Description_En = project.Description_En,
                Description_Ar = project.Description_Ar,
                Place_En = project.Place_En,
                Place_Ar = project.Place_Ar,
                Type = project.Type,
                CreatedAt=project.CreatedAt
                

            };
            return View(_project);
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectViewModel project)
        {
           

                var _project = new Project
                {
                    Id = project.Id,
                    Name_En = project.Name_En,
                    Name_Ar = project.Name_Ar,
                    Description_En = project.Description_En,
                    Description_Ar = project.Description_Ar,
                    Place_En = project.Place_En,
                    Place_Ar = project.Place_Ar,
                    Type = project.Type,
                    CreatedAt=project.CreatedAt

                };
                _projectRepository.Update(_project);
                return RedirectToAction(nameof(Index));
            
           
        }

        // GET: ProjectController/Delete/5
        public ActionResult Delete(int id)
        {
            var project = _projectRepository.GetProject(id);
            var imges = _imageRepository.GetImages(id);
       
            foreach (var elem in imges)
            {
                _imageRepository.Delete(elem);

            };
                
                _projectRepository.Delete(project);
        
            return RedirectToAction("Index");
        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var pro = _projectRepository.GetProject(id);
            _projectRepository.Delete(pro);
            return RedirectToAction("Index");
        }
        public string Upload(IFormFile image, string path)
        {
            if (image == null || image.Length == 0)
                return null;

            var folderName = Path.Combine("upload", path);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            string extension = Path.GetExtension(image.FileName);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string nameToUse = path + DateTime.Now.Ticks.ToString();
            nameToUse = nameToUse.Replace(" ", String.Empty);

            var uniqueFileName = $"{nameToUse}{path}{image.Name}{extension}";
            var dbPath = Path.Combine(folderName, uniqueFileName);

            using (var fileStream = new FileStream(Path.Combine(filePath, uniqueFileName), FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            var Url = $"{dbPath}";

            var result = Url;
            return result.Replace("\\", "/");
        }
    }
}
