using Microsoft.EntityFrameworkCore;
using MILLIMETER_DESIGN.Models.Context;
using MILLIMETER_DESIGN.Models.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Models.Repositories.Implementation
{
    public class AdminRepository : IAdminRepository

    {
        MILLIMETER_DESIGNContext _db;

        public AdminRepository(MILLIMETER_DESIGNContext db)
        {
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _db = db;
        }
        public Admin Login(string email, string password)
        {
            var Admin = _db.Admins.SingleOrDefault(x => x.Email == email && x.Password == password);

            return Admin;
        }
    }
}
