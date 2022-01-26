using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.ViweModel.Image
{
    public class ImageViewModel
    {
        public int Id { set; get; }
        public int? ProjectId { set; get; }
        public IFormFile Image { get; set; }
        public string PathImg { set; get; }
        public DateTime CreatedAt { get; set; }
    }
}
