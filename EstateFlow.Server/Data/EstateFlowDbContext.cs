using Microsoft.EntityFrameworkCore;
using EstateFlow.Server.Domain;

namespace EstateFlow.Server.Data
{
    public class EstateFlowDbContext : DbContext
    {
        public DbSet<City> Cities => Set<City>();
        public DbSet<Property> Properties => Set<Property>();
        public DbSet<PropertyImage> PropertyImages => Set<PropertyImage>();

        public EstateFlowDbContext(DbContextOptions<EstateFlowDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Business ID Code should be unique 
            modelBuilder.Entity<Property>().HasIndex(p => p.Code).IsUnique();

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Budapest" },
                new City { Id = 8, Name = "Kecskemét" }
                );

            modelBuilder.Entity<Property>().HasData(
                new Property
                { 
                    Id = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 
                    Code = "BUD-001", 
                    Name = "Luxury Apartment in Budapest",
                    Address = "123 Main Street, Budapest",
                    SquareMeters = 54,
                    AdvertisementText = "Experience luxury living in the heart of Budapest!",  
                    CityId = 1 
                 },
                new Property
                {
                    Id = new Guid("7c9e6679-7425-40de-944b-e07456789012"),
                    Code = "KEC-001",
                    Name = "Cozy Family Home in Kecskemét",
                    Address = "456 Oak Avenue, Kecskemét",
                    SquareMeters = 120,
                    AdvertisementText = "A perfect home for your family in Kecskemét!",
                    CityId = 8
                }
                );

            modelBuilder.Entity<PropertyImage>().HasData(
                new PropertyImage
                {
                    Id = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    Name = "Hall",
                    FilePath = "uploads/properties/budapest_apartment_hall0.jpg",
                    UploadedAt = new DateTime(2026, 6, 1, 10, 0, 0, DateTimeKind.Utc),
                    PropertyId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e")

                },
                new PropertyImage
                {
                    Id = new Guid("2c9e6679-7425-40de-944b-e07456789012"),
                    Name = "Living Room",
                    FilePath = "uploads/properties/budapest_apartment_livingroom0.jpg",
                    UploadedAt = new DateTime(2026, 2, 11, 10, 12, 0, DateTimeKind.Utc),
                    PropertyId = new Guid("7c9e6679-7425-40de-944b-e07456789012")
                }
            );

        }
    }
}
