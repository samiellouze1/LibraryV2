using LIbrary.Repository.Generic;
using System.ComponentModel.DataAnnotations;

namespace LIbrary.Models
{
    public class FineStatus:IEntityBase
    {
        [Key]
        public string Id { get; set; }
        public bool status { get; set; }
        public string? fineId { get; set; }
        public virtual Fine fine { get; set; } 
    }
}
