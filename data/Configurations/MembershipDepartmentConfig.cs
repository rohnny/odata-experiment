using data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace data.Configurations
{
    public class MembershipDepartmentConfig : IEntityTypeConfiguration<MembershipDepartment>
    {
        public void Configure(EntityTypeBuilder<MembershipDepartment> builder)
        {
            builder.ToTable("MembershipDepartment");
            builder.HasKey("Id");
        }
    }
}
