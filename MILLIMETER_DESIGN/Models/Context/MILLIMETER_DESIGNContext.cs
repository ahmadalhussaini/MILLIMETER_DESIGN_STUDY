using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MILLIMETER_DESIGN.Models.Context
{
    public class MILLIMETER_DESIGNContext : DbContext
    {
       
        public MILLIMETER_DESIGNContext(DbContextOptions<MILLIMETER_DESIGNContext> option) : base(option)
        {


        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ContactRequests> ContactRequestss { get; set; }
        public DbSet<Team> Teams { get; set; }



    }
}
