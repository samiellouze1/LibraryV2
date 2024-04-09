using LIbrary.ViewModels.BookCatalogue;

namespace LIbrary.ViewModels.BorrowBook
{
    public class BorrowBookVM
    {
        public BookReadVM bookReadVM;
        public DateTime startDate = DateTime.Now ;
        public DateTime endDate = DateTime.Now.AddDays(14);
    }
}
