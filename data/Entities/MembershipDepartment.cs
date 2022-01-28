using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.Entities
{
    [Table("MembershipDepartment")]
    public class MembershipDepartment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }

        public virtual MembershipUsers User { get; set; }
        public virtual Department Department { get; set; }
    }
}
