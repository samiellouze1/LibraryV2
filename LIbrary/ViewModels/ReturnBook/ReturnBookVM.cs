using LIbrary.Models;
using LIbrary.ViewModels.BookCatalogue;

namespace LIbrary.ViewModels.ReturnBook
{
    public class ReturnBookVM
    {
        public BookReadVM? bookReadVM { get; set; }
        public string? review { get;set; }
        public int? rating { get;set; }
        public bool confirmation { get; set; }
    }
}
