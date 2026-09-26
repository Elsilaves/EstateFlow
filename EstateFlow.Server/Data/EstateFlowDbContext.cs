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
        }
    }
}
