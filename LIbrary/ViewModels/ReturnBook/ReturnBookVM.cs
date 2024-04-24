using LIbrary.Models;
using LIbrary.ViewModels.BookCatalogue;
using System.ComponentModel.DataAnnotations;

namespace LIbrary.ViewModels.ReturnBook
{
    public class ReturnBookVM
    {
        public BookReadVM? bookReadVM { get; set; }
        public string? review { get;set; }
        public int? rating { get;set; }
        [MustBeTrue(ErrorMessage = "You must confirm the return.")]
        public bool confirmation { get; set; }
    }
    public class MustBeTrueAttribute : ValidationAttribute
    {
        public MustBeTrueAttribute()
        {
            ErrorMessage = "The {0} field must be true.";
        }

        public override bool IsValid(object value)
        {
            return value is bool && (bool)value == true;
        }
    }
}
