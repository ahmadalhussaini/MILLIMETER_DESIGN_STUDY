using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Models.Repositories.Interface
{
   public interface IAdminRepository
    {
        Admin Login(string email, string password);
    }
}
