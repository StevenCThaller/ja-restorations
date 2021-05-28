using backend.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<FurnitureType> FurnitureTypes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<S3Image> Images { get; set; }
        public DbSet<FurnitureImage> FurnitureImages { get; set; }
        public DbSet<AppraisalImage> AppraisalImages { get; set; }
        public DbSet<Appraisal> Appraisals { get; set; }
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
        }
    }

}