using System.Collections.Generic;

namespace data.Entities
{
    public class Department
    {
        public Department()
        {
            DepartmentLocation = new HashSet<DepartmentLocation>();
        }

        public int Id { get; set; }

        public string DepartmentName { get; set; }

        public virtual ICollection<DepartmentLocation> DepartmentLocation { get; set; }
    }
}
