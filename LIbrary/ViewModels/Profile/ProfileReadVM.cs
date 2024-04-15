using LIbrary.ViewModels.BookCatalogue;

namespace LIbrary.ViewModels.Profile
{
    public class ProfileReadVM
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<BookCoverVM> books { get; set; }
        public int numberOfBooksInposession { get; set; }
        public int numberOfBorrowedBooks { get; set; }
        public DateTime JoinedOn { get; set; }

    }
}
