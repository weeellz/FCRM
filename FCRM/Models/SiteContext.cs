using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FSRM.Models
{
    public class SiteContext : DbContext
    {
        public SiteContext()
            : base("DefaultConnection")
        { }

        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Specification> Tasks { get; set; }
    }
}