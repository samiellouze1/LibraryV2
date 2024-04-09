using LIbrary.ViewModels.BorrowBook;

namespace LIbrary.Services.ReturnBook
{
    public interface IBorrowBookService
    {
        public Task BorrowBook(BorrowBookVM borrowBookVM, string readerId);    
    }
}
