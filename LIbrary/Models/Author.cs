using LIbrary.Repository.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIbrary.Models
{
    public class Author:IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string? name { get; set; }
        public virtual ICollection<Book> books { get; set; } 
    }
}
