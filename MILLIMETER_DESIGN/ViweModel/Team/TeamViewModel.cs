using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.ViweModel.Team
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public IFormFile Image { get; set; }
        public string PathImg { get; set; }
    }
}
