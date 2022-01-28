using System.Data.Entity;
using data.Entities;
using NLog;

namespace data
{
    public class AppDbContext : DbContext
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public AppDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public virtual DbSet<MembershipUsers> MembershipUsers { get; set; }
        public virtual DbSet<MembershipDepartment> MembershipDepartments { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentLocation> DepartmentLocation { get; set; }
        public virtual DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasMany(e => e.DepartmentLocation)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.DepartmentLocation)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);
        }
    }
}
