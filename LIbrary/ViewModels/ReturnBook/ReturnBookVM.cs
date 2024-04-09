using LIbrary.Models;
using LIbrary.ViewModels.BookCatalogue;

namespace LIbrary.ViewModels.ReturnBook
{
    public class ReturnBookVM
    {
        public BookReadVM bookReadVM { get; set; }
        public ReviewRating reviewRating {  get; set; } = new ReviewRating();
        public bool confirmation { get; set; }
    }
}
