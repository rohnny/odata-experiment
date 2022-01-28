using System;
using System.Linq;
using System.Linq.Expressions;
using data.DataAccess;
using data.Entities;

namespace services
{
    public interface IUserService
    {
        IQueryable<MembershipUsers> GetUsers(Expression<Func<MembershipUsers, bool>> filter = null);
    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<MembershipUsers> GetUsers(Expression<Func<MembershipUsers, bool>> filter = null)
        {
            return _unitOfWork.MembershipUsersRepository.Get(filter);
        }

    }

}
