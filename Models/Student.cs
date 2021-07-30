using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_api.Models
{
    [Table("Student")]
    public partial class Student
    {
        public Student() {
            Classes = new HashSet<Class>();
        }
        [Key]
        public int Id {get; set;}
        public string Name {get; set;}
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:yyyy-MM-dd", ApplyFormatInEditMode =true)]
        public string DateOfBirth {get; set;}
        public float AverageMark {get; set;}
        public virtual ICollection<Class> Classes {get; set;}
    }
}