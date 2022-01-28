using System.ComponentModel.DataAnnotations;

namespace data.Entities
{
    public class MembershipDepartment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }

        public virtual MembershipUsers User { get; set; }
        public virtual Department Department { get; set; }
    }
}
