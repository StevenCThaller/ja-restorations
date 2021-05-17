using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<FurnitureType> FurnitureTypes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<S3Image> Images { get; set; }
        public DbSet<FurnitureImage> FurnitureImages { get; set; }
        public DbSet<AppraisalImage> AppraisalImages { get; set; }
        public DbSet<Appraisal> Appraisals { get; set; }
        public DbSet<FurnitureHasColor> FurnitureHasColors { get; set; }
        
    }
}