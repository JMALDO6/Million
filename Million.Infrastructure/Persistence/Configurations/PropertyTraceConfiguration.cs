using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Million.Domain.Entities;

namespace Million.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the PropertyTrace entity
    /// </summary>
    public class PropertyTraceConfiguration : IEntityTypeConfiguration<PropertyTrace>
    {
        public void Configure(EntityTypeBuilder<PropertyTrace> builder)
        {
            builder.HasKey(pi => pi.IdPropertyTrace);

            builder.Property(pi => pi.DateSale)
                   .IsRequired();

            builder.Property(pi => pi.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(pi => pi.Value)
                   .IsRequired();

            builder.Property(pi => pi.Tax)
                   .IsRequired();

            builder.HasOne(pi => pi.Property)
                   .WithMany(p => p.PropertyTraces)
                   .HasForeignKey(pi => pi.IdProperty);

            builder.ToTable("PropertyTraces");
        }
    }
}