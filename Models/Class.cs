using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_api.Models
{
    [Table("Class")]
    public partial class Class
    {
        public Class() {
            ClassStudents = new HashSet<ClassStudent>();
        }
        [Key]
        public int ClassId {get; set;}
        [Required]
        [StringLength(20)]
        public string ClassName {get; set;}
        public virtual ICollection<ClassStudent> ClassStudents {get; set;}
    }
}