using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace net_core_api.DatabaseFirstModels
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [InverseProperty(nameof(Book.Category))]
        public virtual ICollection<Book> Books { get; set; }
    }
}
