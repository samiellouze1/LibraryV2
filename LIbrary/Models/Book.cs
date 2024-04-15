using LIbrary.Repository.Generic;
using LIbrary.Repository.Specific;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIbrary.Models
{
    public class Book:IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime dateOfCreation { get; set; }
        public string coverUrl { get; set; }
        public int price { get; set; }
        public string authorId { get; set; }
        public virtual Author author { get; set; } 
        public string genreId { get; set; }
        public virtual Genre genre { get; set; } 
        public virtual ICollection<BookCopy> bookCopies { get; set; }
    }
}
