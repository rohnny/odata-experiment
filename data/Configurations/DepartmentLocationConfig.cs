using data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace data.Configurations
{
    public class DepartmentLocationConfig : IEntityTypeConfiguration<DepartmentLocation>
    {
        public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
        {
            builder.ToTable("DepartmentLocation");
            builder.HasKey("Id");
        }
    }
}
