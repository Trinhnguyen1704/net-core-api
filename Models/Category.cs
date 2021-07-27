using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_api.Models
{
    [Table("Category")]
    public partial class Category
    {
        public Category(){
            Books = new HashSet<Book>();
        }
        [Key]
        public int CategoryId{get; set;}
        public string CategoryName {get; set;}
        public virtual ICollection<Book> Books {get; set;}
    }
}