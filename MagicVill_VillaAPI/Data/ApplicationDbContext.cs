using MagicVill_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVill_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Test",
                    Details = "Multiple Active Result Sets (MARS) is a feature that works with SQL Server to allow the execution of multiple batches on a single connection. When MARS is enabled for use with SQL Server, each command object used adds a session to the connection",
                    ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg",
                    Occupancy = 5,
                    Rate = 5,
                    Amenity = "",
                    Sqft = 5,
                    CreatedDate = DateTime.Now,
                    UpdateDate = DateTime.Now,


                });
        }
    }
}
