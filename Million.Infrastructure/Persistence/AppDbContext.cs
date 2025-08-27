using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Million.Domain.Entities;
using Million.Infrastructure.Persistence.Configurations;

namespace Million.Infrastructure.Persistence
{
    /// <summary>
    /// Application database context integrating Identity and application entities.
    /// </summary>
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<PropertyTrace> PropertyTraces { get; set; }

        /// <summary>
        /// Model creation and entity configuration.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PropertyConfiguration());
            builder.ApplyConfiguration(new OwnerConfiguration());
            builder.ApplyConfiguration(new PropertyImageConfiguration());
            builder.ApplyConfiguration(new PropertyTraceConfiguration());
        }
    }
}