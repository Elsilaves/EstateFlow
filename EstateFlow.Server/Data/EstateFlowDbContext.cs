using Microsoft.EntityFrameworkCore;
using EstateFlow.Server.Domain;

namespace EstateFlow.Server.Data
{
    public class EstateFlowDbContext : DbContext
    {
        public DbSet<City> Cities => Set<City>();
        public DbSet<Property> Properties => Set<Property>();
        public DbSet<PropertyImage> PropertyImages => Set<PropertyImage>();
        public DbSet<Meter> Meters => Set<Meter>();
        public DbSet<UtilityType> UtilityTypes => Set<UtilityType>();
        public DbSet<Provider> Providers => Set<Provider>();
        public DbSet<MeterReading> MeterReadings => Set<MeterReading>();
        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<Rental> Rentals => Set<Rental>();

        public EstateFlowDbContext(DbContextOptions<EstateFlowDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Business ID Code should be unique 
            modelBuilder.Entity<Property>().HasIndex(p => p.Code).IsUnique();

            //Restrict delete behavior. Entities with foreign key relationships will not be deleted if they are referenced by other entities.
            modelBuilder.Entity<Provider>().HasOne(p => p.UtilityType).WithMany(t => t.Providers)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Meter>().HasOne(m => m.UtilityType).WithMany().HasForeignKey(m => m.UtilityTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Meter>().HasOne(m => m.Provider).WithMany().HasForeignKey(m => m.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            //Cascade delete behavior. Entities with foreign key relationships will be deleted if the principal entity is deleted.
            modelBuilder.Entity<MeterReading>().HasOne(r => r.Meter).WithMany(m => m.MeterReadings).HasForeignKey(r => r.MeterId)
                .OnDelete(DeleteBehavior.Cascade);

            // Set precision for decimal properties. maximum 18 digits, 2 decimal places
            modelBuilder.Entity<Rental>().Property(r => r.MonthlyRent).HasPrecision(18, 2);
            modelBuilder.Entity<MeterReading>().Property(r => r.Value).HasPrecision(18, 3);


            //Seed data
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

            modelBuilder.Entity<UtilityType>().HasData(
                new UtilityType { Id = 1, Name = "Electricity" },
                new UtilityType { Id = 2, Name = "Water" },
                new UtilityType { Id = 3, Name = "Gas" }
            );

            modelBuilder.Entity<Provider>().HasData(
                new Provider { Id = 1, Name = "Electricity Provider A", UtilityTypeId = 1 },
                new Provider { Id = 2, Name = "Water Provider B", UtilityTypeId = 2 },
                new Provider { Id = 3, Name = "Gas Provider C", UtilityTypeId = 3 }
            );

            modelBuilder.Entity<Meter>().HasData(
                new Meter 
                { 
                    Id = new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3301"), 
                    Name = "Electricity Meter", 
                    Location = "Living Room", 
                    SerialNumber = "ELEC-001", 
                    ReadingPeriod = "Monthly", 
                    Note = "Need to be validated in 2007", 
                    UtilityTypeId = 1, 
                    PropertyId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 
                    ProviderId = 1 
                },
                new Meter 
                { 
                    Id = new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3302"), 
                    Name = "Water Meter",
                    Location = "Kitchen", 
                    SerialNumber = "WATER-001", 
                    ReadingPeriod = "Quarterly",
                    Note = "Need to be validated in 2007", 
                    UtilityTypeId = 2, 
                    PropertyId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 
                    ProviderId = 2 
                },
                new Meter 
                { 
                    Id = new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3303"), 
                    Name = "Gas Meter", 
                    Location = "Garage", 
                    SerialNumber = "GAS-001", 
                    ReadingPeriod = null, 
                    Note = "Need to be validated in 2007 ", 
                    UtilityTypeId = 3, 
                    PropertyId = new Guid("7c9e6679-7425-40de-944b-e07456789012"), 
                    ProviderId = 3 
                }
            );

            modelBuilder.Entity<MeterReading>().HasData(
                new MeterReading
                {
                    Id = new Guid("4f2504e0-4f89-41d3-9a0c-0305e82c3304"),
                    MesuredAt = new DateTime(2026, 6, 1, 7, 40, 0, DateTimeKind.Utc),
                    ReportedAt = new DateTime(2026, 6, 1, 8, 0, 0, DateTimeKind.Utc),
                    ApprovedAt = new DateTime(2026, 6, 1, 9, 0, 0, DateTimeKind.Utc),
                    PhotoPath = "uploads/meter_readings/electricity_reading_2026-06-01.jpg",
                    MeterId = new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3301"),
                    Value = 120.5m,
                },
                new MeterReading
                {
                    Id = new Guid("4f2504e0-4f89-41d3-9a0c-0305e82c3305"),
                    MesuredAt = new DateTime(2026, 6, 1, 7, 45, 0, DateTimeKind.Utc),
                    ReportedAt = new DateTime(2026, 6, 1, 8, 5, 0, DateTimeKind.Utc),
                    ApprovedAt = new DateTime(2026, 6, 1, 9, 5, 0, DateTimeKind.Utc),
                    PhotoPath = "uploads/meter_readings/water_reading_2026-06-01.jpg",
                    MeterId = new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3302"),
                    Value = 85.2m,
                },
                new MeterReading
                {
                    Id = new Guid("4f2504e0-4f89-41d3-9a0c-0305e82c3306"),
                    MesuredAt = new DateTime(2026, 6, 1, 10, 15, 0, DateTimeKind.Utc),
                    ReportedAt = new DateTime(2026, 6, 1, 10, 30, 0, DateTimeKind.Utc),
                    ApprovedAt = new DateTime(2026, 6, 1, 11, 0, 0, DateTimeKind.Utc),
                    PhotoPath = "uploads/meter_readings/gas_reading_2026-06-01.jpg",
                    MeterId = new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3303"),
                    Value = 65.8m,
                }
            );

            modelBuilder.Entity<Tenant>().HasData(
                new Tenant
                {
                    Id = new Guid("5f2504e0-4f89-41d3-9a0c-0305e82c3307"),
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = null
                },
                new Tenant
                {
                    Id = new Guid("5f2504e0-4f89-41d3-9a0c-0305e82c3308"),
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "+00 00 123 4567"
                }
            );
        
            modelBuilder.Entity<Rental>().HasData(
                new Rental
                {
                    Id = new Guid("6f2504e0-4f89-41d3-9a0c-0305e82c3309"),
                    PropertyId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    TenantId = new Guid("5f2504e0-4f89-41d3-9a0c-0305e82c3307"),
                    StartDate = new DateTime(2025, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2026, 8, 31, 0, 0, 0, DateTimeKind.Utc),
                    MonthlyRent = 120000m
                },
                new Rental
                {
                    Id = new Guid("6f2504e0-4f89-41d3-9a0c-0305e82c3310"),
                    PropertyId = new Guid("7c9e6679-7425-40de-944b-e07456789012"),
                    TenantId = new Guid("5f2504e0-4f89-41d3-9a0c-0305e82c3308"),
                    StartDate = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                    EndDate = null,
                    MonthlyRent = 150000m
                }
            );

        }
    }
}
