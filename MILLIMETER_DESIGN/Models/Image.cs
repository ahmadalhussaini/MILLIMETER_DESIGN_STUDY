using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Models
{
    public class Image
    {
        
        public int Id { set; get; }
        public Project Project { set; get; }
        public int? ProjectId { set; get; }
        public string PathImg { set; get; }
        public DateTime CreatedAt { get; set; }


    }
}
