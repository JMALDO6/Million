using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Million.Domain.Entities;

namespace Million.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the Owner entity
    /// </summary>
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasKey(o => o.IdOwner);

            builder.Property(o => o.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(o => o.Address)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(o => o.Photo)
                   .HasMaxLength(255)
                   .IsRequired(false);

            builder.Property(o => o.Birthday)
                   .IsRequired(false);

            builder.HasMany(o => o.Properties)
                   .WithOne(p => p.Owner)
                   .HasForeignKey(p => p.IdOwner);

            builder.ToTable("Owners");
        }
    }
}