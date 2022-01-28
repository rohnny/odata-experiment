using System.Threading.Tasks;
using data.Entities;

namespace data.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            AppDbContext dbContext,
            IRepository<Location> locationRepository,
            IRepository<MembershipUsers> membershipUsersRepository,
            IRepository<Department> departmentRepository,
            IRepository<MembershipDepartment> membershipDepartmentsRepository)
        {
            MembershipDepartmentsRepository = membershipDepartmentsRepository;
            LocationRepository = locationRepository;
            MembershipUsersRepository = membershipUsersRepository;
            DepartmentRepository = departmentRepository;
            AppDbContext = dbContext;
        }

        public AppDbContext AppDbContext { get; set; }
        public IRepository<Location> LocationRepository { get; }

        public IRepository<MembershipUsers> MembershipUsersRepository { get; }

        public IRepository<Department> DepartmentRepository { get; }

        public IRepository<MembershipDepartment> MembershipDepartmentsRepository { get; }

        public Task SaveAsync()
        {
            return AppDbContext.SaveChangesAsync();
        }
    }
}
