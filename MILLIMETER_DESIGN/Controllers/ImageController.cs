using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MILLIMETER_DESIGN.Models;
using MILLIMETER_DESIGN.Models.Repositories.Implementation;
using MILLIMETER_DESIGN.Models.Repositories.Interface;
using MILLIMETER_DESIGN.ViweModel.Image;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        // GET: ImageController
        public ActionResult Index()
        {
            var images = _imageRepository.GetImages();
            var List = new ImageListViewModel();
            var _images = new List<ImageViewModel>();
            foreach (var elem in images)
            {
                var model = new ImageViewModel
                {
                    Id = elem.Id,
                    ProjectId=elem.ProjectId,
                    PathImg=elem.PathImg,
                    CreatedAt=elem.CreatedAt      
                };
                _images.Add(model);

            }
            List.images = _images;
            return View(List);
        }

        // GET: ImageController/Details/5
        public ActionResult Details(int id)
        {
            var img = _imageRepository.GetImg(id);
            var model = new ImageViewModel
            {
                Id = img.Id,
                ProjectId=img.ProjectId,
                PathImg=img.PathImg,
                CreatedAt=img.CreatedAt

            };

            return View(model);

        }

        // GET: ImageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImageViewModel img)
        {
            var path = Upload(img.Image, "img");
            var _img = new Image
            {
                PathImg = path,
                ProjectId=img.ProjectId,
                CreatedAt=img.CreatedAt


            };
            _imageRepository.Add(_img);

            return RedirectToAction(nameof(Index));
        }

        // GET: ImageController/Edit/5
        public ActionResult Edit(int id)
        {
            var img = _imageRepository.GetImg(id);
            var _image = new ImageViewModel
            {
               Id=img.Id,
               ProjectId=img.ProjectId,
               PathImg=img.PathImg,
               CreatedAt=img.CreatedAt

            };
            return View(_image);
        }

        // POST: ImageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ImageViewModel img)
        {
            var image = _imageRepository.GetImg(img.Id);
            var Path = image.PathImg;
            if (img.Image != null) {
                Path = Upload(img.Image, "img");
            }
            var _img = new Image
            {
                Id = img.Id,
                ProjectId=img.ProjectId,
                PathImg = Path,
                CreatedAt=img.CreatedAt

            };
            _imageRepository.Update(_img);
            return RedirectToAction(nameof(Index));
        }

        // GET: ImageController/Delete/5
        public ActionResult Delete(int id)
        {
            var img = _imageRepository.GetImg(id);
            _imageRepository.Delete(img);
            return RedirectToAction("Index");
        }

        // POST: ImageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var img = _imageRepository.GetImg(id);
            _imageRepository.Delete(img);
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
