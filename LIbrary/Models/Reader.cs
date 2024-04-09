using LIbrary.Repository.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIbrary.Models
{
    public class Reader: IdentityUser, IEntityBase
    {
        public string ImageUrl { get; set; }
        public virtual ICollection<BorrowItem> borrowItems { get; set; } = new List<BorrowItem>();
    }
}
