using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Million.Domain.Entities;

namespace Million.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the PropertyImage entity
    /// </summary>
    public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
    {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.HasKey(pi => pi.IdPropertyImage);

            builder.Property(pi => pi.File)
                   .HasColumnType("varbinary(max)")
                    .IsRequired();

            builder.Property(pi => pi.Enabled)
                   .IsRequired();

            builder.HasOne(pi => pi.Property)
                   .WithMany(p => p.PropertyImages)
                   .HasForeignKey(pi => pi.IdProperty);

            builder.ToTable("PropertyImages");
        }
    }
}