using data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace data.Configurations
{
    public class LocationConfig : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location");
            builder.HasKey("Id");

            builder.HasMany<DepartmentLocation>(e => e.DepartmentLocation)
                .WithOne(e => e.Location);
        }
    }
}
