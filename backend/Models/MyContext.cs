using backend.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FurnitureLike> FurnitureLikes { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<FurnitureType> FurnitureTypes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<S3Image> Images { get; set; }
        public DbSet<FurnitureImage> FurnitureImages { get; set; }
        public DbSet<AppraisalImage> AppraisalImages { get; set; }
        public DbSet<Appraisal> Appraisals { get; set; }
        public DbSet<AppraisalStatus> AppraisalStatus { get; set; }
        public DbSet<FurnitureHasColor> FurnitureHasColors { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                        .HasData(
                            new Role { roleId = 1, role = "Basic" },
                            new Role { roleId = 2, role = "Employee" },
                            new Role { roleId = 3, role = "Administrator" }
                        );
            
            modelBuilder.Entity<FurnitureType>()
                        .HasData(
                            new FurnitureType { typeId = 1, name = "nighstand" },
                            new FurnitureType { typeId = 2, name = "coffee table" },
                            new FurnitureType { typeId = 3, name = "dresser" }
                        );

            modelBuilder.Entity<AppraisalStatus>()
                        .HasData(
                            new AppraisalStatus { appraisalStatusId = 1, name = "sent", description = "You have sent your appraisal request."},
                            new AppraisalStatus { appraisalStatusId = 2, name = "received", description = "Your appraisal request has been received by our team!" },
                            new AppraisalStatus { appraisalStatusId = 3, name = "approved", description = "Your appraisal has been approved!" },
                            new AppraisalStatus { appraisalStatusId = 4, name = "completed", description = "Our team has already purchased this item from you."},
                            new AppraisalStatus { appraisalStatusId = 5, name = "canceled", description = "You and our team were unable to agree upon a price. It happens!" },
                            new AppraisalStatus { appraisalStatusId = 6, name = "denied", description = "Your appraisal has been denied." }
                        );
                        
            modelBuilder.Entity<RefurbStatus>()
                        .HasData(
                            new RefurbStatus { refurbStatusId = 1, name = "sent", description = "You have sent your refurbish request."},
                            new RefurbStatus { refurbStatusId = 2, name = "received", description = "Your refurbish request has been received by our team!" },
                            new RefurbStatus { refurbStatusId = 3, name = "approved", description = "Your refurbish has been approved!" },
                            new RefurbStatus { refurbStatusId = 4, name = "transfer in", description = "Your refurbish has been approved and our team is working with you to find out how to get your furniture piece to us!"},
                            new RefurbStatus { refurbStatusId = 5, name = "in queue", description = "Our team is working hard to finish other projects before moving on to yours!"},
                            new RefurbStatus { refurbStatusId = 6, name = "in progress", description = "Our team is working to refurbish your furniture!"},
                            new RefurbStatus { refurbStatusId = 7, name = "transfer out", description = "Our team has completed the refurbishment and are working to get your piece back to you!" },
                            new RefurbStatus { refurbStatusId = 8, name = "complete", description = "Enjoy your newly refurbished furniture!" },
                            new RefurbStatus { refurbStatusId = 9, name = "denied", description = "Your refurbishment has been denied." }
                        );
        }
    }

}