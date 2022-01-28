using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.Entities
{
    [Table("MembershipUsers")]
    public class MembershipUsers
    {
        public MembershipUsers()
        {
            Departments = new HashSet<MembershipDepartment>();
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual ICollection<MembershipDepartment> Departments { get; set; }
    }
}
