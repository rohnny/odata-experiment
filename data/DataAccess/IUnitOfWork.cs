using System.Threading.Tasks;
using data.Entities;

namespace data.DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<Location> LocationRepository { get; }
        IRepository<MembershipUsers> MembershipUsersRepository { get; }

        IRepository<Department> DepartmentRepository { get; }
        IRepository<MembershipDepartment> MembershipDepartmentsRepository { get; }

        Task SaveAsync();
    }
}
