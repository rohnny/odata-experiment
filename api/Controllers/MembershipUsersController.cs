using System.Linq;
using data.DataAccess;
using data.Entities;
using Microsoft.AspNet.OData;
using services;

namespace api.Controllers
{
    public class MembershipUsersController : ODataController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public MembershipUsersController(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        [EnableQuery(MaxExpansionDepth = 5)]
        public IQueryable<MembershipUsers> Get() => _userService.GetUsers();

        [EnableQuery(MaxExpansionDepth = 5)]
        public SingleResult<MembershipUsers> Get([FromODataUri]int key) => SingleResult.Create(_userService.GetUsers(l => l.Id == key));
    }
}




