using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.Entities
{
    [Table("Department")]
    public class Department
    {
        public Department()
        {
            DepartmentLocation = new HashSet<DepartmentLocation>();
        }

        [Key]
        public int Id { get; set; }

        public string DepartmentName { get; set; }

        public virtual ICollection<DepartmentLocation> DepartmentLocation { get; set; }
    }
}
