using LIbrary.Repository.Generic;
using System.ComponentModel.DataAnnotations;

namespace LIbrary.Models
{
    public class Fine: IEntityBase
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public int NumberOfDays { get; set; }
        public string? borrowItemId { get; set; }
        public virtual BorrowItem borrowItem { get; set; } 
        public string? fineStatusId { get; set; }
        public virtual FineStatus fineStatus { get; set; }
    }
}
