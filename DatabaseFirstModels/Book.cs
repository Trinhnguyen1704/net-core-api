using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace net_core_api.DatabaseFirstModels
{
    [Table("Book")]
    [Index(nameof(CategoryId), Name = "IX_Book_CategoryId")]
    public partial class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Books")]
        public virtual Category Category { get; set; }
    }
}
