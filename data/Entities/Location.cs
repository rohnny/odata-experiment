using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.Entities
{
    public class Location
    {
        public Location()
        {
            DepartmentLocation = new HashSet<DepartmentLocation>();
        }

        public int Id { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<DepartmentLocation> DepartmentLocation { get; set; }
    }
}
