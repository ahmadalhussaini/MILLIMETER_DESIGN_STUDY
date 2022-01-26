using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Models.Repositories.Interface
{
  public interface IImageRepository
    {
        Image GetImg(int Id);
        List<Image> GetImages();
        List<Image> GetImages(int ProjectId);
        Image Add(Image image);
        void Update(Image image);
        void Delete(Image image);
    }
}
