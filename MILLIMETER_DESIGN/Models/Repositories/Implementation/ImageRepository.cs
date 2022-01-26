using Microsoft.EntityFrameworkCore;
using MILLIMETER_DESIGN.Models.Context;
using MILLIMETER_DESIGN.Models.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Models.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        MILLIMETER_DESIGNContext _db;

        public ImageRepository(MILLIMETER_DESIGNContext db)
        {
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _db = db;

        }
        public Image Add(Image entity)
        {
            var image = _db.Images.Add(entity);
            _db.SaveChanges();
            return image.Entity;
        }

        public void Update(Image entity)
        {
            _db.Images.Update(entity);
            _db.SaveChanges();
        }

        public void Delete(Image entity)
        {
            _db.Images.Remove(entity);
            _db.SaveChanges();
        }
        public Image GetImg(int Id)
        {
            var img = _db.Images.SingleOrDefault(b => b.Id == Id);
            return img;
        }

        public List<Image> GetImages(int ProjectId)
        {
            var img = _db.Images.Where(x => x.ProjectId == ProjectId).ToList();
            return img;
        }

        public List<Image> GetImages()
        {
            var img = _db.Images.ToList();
            return img;
        }
    }
}
