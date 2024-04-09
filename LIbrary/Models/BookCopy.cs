using LIbrary.Repository.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIbrary.Models
{
    public class BookCopy : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string? bookId { get; set; }
        public virtual Book book { get; set; }
        public virtual ICollection<BorrowItem> borrowItems { get; set; } = new List<BorrowItem>();
        // many to many

    }
}
