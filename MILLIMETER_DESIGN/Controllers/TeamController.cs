using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MILLIMETER_DESIGN.Models;
using MILLIMETER_DESIGN.Models.Repositories.Interface;
using MILLIMETER_DESIGN.ViweModel.Team;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        private ITeamRepository _teamRepository;

        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        public IActionResult Index()
        {
            var team = _teamRepository.GetTeams();
            var List = new TeamListViewModel();
            var _team = new List<TeamViewModel>();
            foreach (var elem in team)
            {
                var model = new TeamViewModel
                {
                    Id = elem.Id,
                    Name = elem.Name,
                    Position = elem.Position,
                    Description = elem.Description,
                    Country=elem.Country,
                    PathImg=elem.PathImg

                };

                _team.Add(model);

            }
            List.TeamViewModel = _team;
            return View(List);
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamViewModel team)
        {
            var path = Upload(team.Image, "img");
            var _team = new Team
            {
                Name=team.Name,
                Position=team.Position,
                Description=team.Description,
                Country=team.Country,
                PathImg = path
            };
            _teamRepository.Add(_team);

            return RedirectToAction(nameof(Index));
        }
        public ActionResult Details(int id)
        {
            var team = _teamRepository.GetTeam(id);
            var model = new TeamViewModel
            {
                Id = team.Id,
                Name=team.Name,
                Position=team.Position,
                Description=team.Description, 
                Country=team.Country,
                PathImg = team.PathImg,
               

            };

            return View(model);

        }
        public ActionResult Edit(int id)
        {
            var team = _teamRepository.GetTeam(id);
            var _team = new TeamViewModel
            {
                Id = team.Id,
                Name=team.Name,
                Position=team.Position,
                Description=team.Description,
                Country=team.Country,
                PathImg = team.PathImg,
               

            };
            return View(_team);
        }

        // POST: ImageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamViewModel team)
        {
            var teams = _teamRepository.GetTeam(team.Id);
            var Path = teams.PathImg;
            if (team.Image != null)
            {
                Path = Upload(team.Image, "img");
            }
            var _team = new Team
            {
                Id = team.Id,
                Name=team.Name,
                Position=team.Position,
                Description=team.Description,
                Country=team.Country,
                PathImg = Path,
              

            };
            _teamRepository.Update(_team);
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Delete(int id)
        {
            var team = _teamRepository.GetTeam(id);
            _teamRepository.Delete(team);
            return RedirectToAction("Index");
        }

        // POST: ImageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var team = _teamRepository.GetTeam(id);
            _teamRepository.Delete(team);
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
