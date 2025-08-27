using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Million.Infrastructure.Persistence.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Domain.Entities.Property>

    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Property> builder)
        {
            builder.HasKey(p => p.IdProperty);

            builder.HasOne(p => p.Owner)
                   .WithMany(o => o.Properties)
                   .HasForeignKey(p => p.IdOwner);
        }
    }
}