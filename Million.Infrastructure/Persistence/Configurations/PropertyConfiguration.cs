using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Million.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the Property entity
    /// </summary>
    public class PropertyConfiguration : IEntityTypeConfiguration<Domain.Entities.Property>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Property> builder)
        {
            builder.HasKey(p => p.IdProperty);

            builder.Property(o => o.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(o => o.Address)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(o => o.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.CodeInternal)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(o => o.Year)
                   .IsRequired()
                   .HasMaxLength(4);

            builder.HasOne(p => p.Owner)
                   .WithMany(o => o.Properties)
                   .HasForeignKey(p => p.IdOwner);

            builder.ToTable("Properties");
        }
    }
}