using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_api.Models
{
    [Table("Book")]
    public partial class Book
    {
        [Key]
        public int  Id {get;set;}
        [Required]
        public string Title {get; set;}
        public string Author {get;set ;}
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:yyyy-MM-dd", ApplyFormatInEditMode =true)]
        public string PublicationDate {get; set;}
        [Required]
        public bool IsAvailable {get;set;}
        public string Description {get;set;}
        public int? CategoryId {get;set;}
        [ForeignKey("CategoryId")]

        public virtual Category CategoryIdNavigation {get; set;}
    }
}