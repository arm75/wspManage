using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WSPManage.Models
{
    public class WSPManageContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public WSPManageContext() : base("name=WSPManageContext")
        {
        }

        public System.Data.Entity.DbSet<WSPManage.Models.customer> customers { get; set; }
        public System.Data.Entity.DbSet<WSPManage.Models.property> properties { get; set; }
        public System.Data.Entity.DbSet<WSPManage.Models.businessEntity> businessEntities { get; set; }
        public System.Data.Entity.DbSet<WSPManage.Models.loan> loans { get; set; }
        public System.Data.Entity.DbSet<WSPManage.Models.rental> rentals { get; set; }
        public System.Data.Entity.DbSet<WSPManage.Models.loanPayment> loanPayments { get; set; }
        public System.Data.Entity.DbSet<WSPManage.Models.rentalPayment> rentalPayments { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(18, 10));
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        public void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
                ? HttpContext.Current.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).DateCreated = DateTime.Now;
                    ((BaseEntity)entity.Entity).UserCreated = currentUsername;
                }

                ((BaseEntity)entity.Entity).DateModified = DateTime.Now;
                ((BaseEntity)entity.Entity).UserModified = currentUsername;
            }
        }


    }
}
