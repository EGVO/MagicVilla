using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Villa> Villas { get; set; }

        public DbSet<NumberVilla> NumberVillas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Villa Real",
                    Detail = "Detalle de la Villa...",
                    UrlImage = "",
                    Occupants = 5,
                    SquareMeter = 50,
                    Rate = 200,
                    Amenity = "",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                },
                new Villa()
                {
                    Id = 2,
                    Name = "Premium Vista a la Piscina",
                    Detail = "Detalle de la Villa...",
                    UrlImage = "",
                    Occupants = 4,
                    SquareMeter = 40,
                    Rate = 150,
                    Amenity = "",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                }
            );
        }
    }
}
