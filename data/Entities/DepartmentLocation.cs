
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.Entities
{
    [Table("DepartmentLocation")]
    public class DepartmentLocation
    {
        [Key]
        public int Id { get; set; }

        public int DepartmentId { get; set; }
        public int LocationId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Location Location { get; set; }
    }
}
