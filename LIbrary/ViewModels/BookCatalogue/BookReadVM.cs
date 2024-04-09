using LIbrary.Models;

namespace LIbrary.ViewModels.BookCatalogue
{
    public class BookReadVM
    {
        public string? Id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public DateTime dateOfCreation { get; set; }
        public string? coverUrl { get; set; }
        public int? price { get; set; }
        public string? authorName { get; set; }
        public string? genreName { get; set; }
        public bool isAlreadyBorrowed { get; set; }
        public int numberOfCopies { get; set; }
        public int averageRating { get; set; }
        public List<ReviewRating> reviewRatings { get; set; }
    }
}
