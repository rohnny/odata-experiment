using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.Entities
{
    [Table("Location")]
    public class Location
    {
        public Location()
        {
            DepartmentLocation = new HashSet<DepartmentLocation>();
        }

        [Key]
        public int Id { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<DepartmentLocation> DepartmentLocation { get; set; }
    }
}
