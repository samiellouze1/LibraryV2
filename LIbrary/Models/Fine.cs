using LIbrary.Repository.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIbrary.Models
{
    public class Fine: IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string? borrowItemId { get; set; }
        public virtual BorrowItem borrowItem { get; set; } 
        public string? fineStatusId { get; set; }
        public virtual FineStatus fineStatus { get; set; }
    }
}
