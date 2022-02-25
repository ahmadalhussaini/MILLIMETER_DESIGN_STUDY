using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Ar { get; set; }
        public string Description_En { get; set; }
        public string Description_Ar { get; set; }
        public string Place_En { get; set; }
        public string Place_Ar { get; set; }
        public string Type { get; set; }
        public List<Image> Images { set; get; }
        public DateTime CreatedAt { get; set; }

      
    }
}
