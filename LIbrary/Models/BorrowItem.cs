using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LIbrary.Repository.Generic;

namespace LIbrary.Models
{
    public class BorrowItem:IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime supposedEndDate { get; set; } 
        public string? bookCopyId { get; set; }
        public virtual BookCopy bookCopy { get; set; }
        public string? readerId { get; set; }
        public virtual Reader reader { get; set; }
        public string? borrowItemStatusId { get; set; }
        public virtual BorrowItemStatus borrowItemStatus { get; set; } 
        public string? reviewRatingId { get; set; }
        public virtual ReviewRating reviewRating { get; set; }
        public string? fineId { get; set; }
        public virtual Fine fine { get; set; } 
    }
}
