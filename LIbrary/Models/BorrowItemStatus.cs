using LIbrary.Repository.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LIbrary.Models
{
    public class BorrowItemStatus : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string name { get; set; }
        public virtual ICollection<BorrowItem> borrowItems { get; set; }
    }
}
