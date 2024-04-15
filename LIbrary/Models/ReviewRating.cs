using LIbrary.Repository.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIbrary.Models
{
    public class ReviewRating:IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string? review { get; set; }
        public int? rating { get; set; }
        public string? borrowItemId { get; set; }
        public virtual BorrowItem borrowItem { get; set; } 
    }
}
