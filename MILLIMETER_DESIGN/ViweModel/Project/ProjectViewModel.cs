using Microsoft.AspNetCore.Http;
using MILLIMETER_DESIGN.ViweModel.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.ViweModel.Project
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Ar { get; set; }
        public string Description_En { get; set; }
        public string Description_Ar { get; set; }
        public string Place_En { get; set; }
        public string Place_Ar { get; set; }
        public string Type { get; set; }
        public List<IFormFile> _images { get; set; }
        public List<ImageViewModel> images { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
