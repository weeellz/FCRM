using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace FCRM.Web.Models
{
    public class SiteContext : IdentityDbContext<User>
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<Work> Works { get; set; }

        public DbSet<Specification> Specifications { get; set; }

        public DbSet<Company> Companies { get; set; }

        public SiteContext()
            : base("DefaultConnection")
        {
            //Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            //this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Conventions.Add<ForeignKeyNamingConvention>();
        }
    }
}