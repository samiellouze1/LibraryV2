using LIbrary.Models;

namespace LIbrary.Services.BookCatalogue
{
    public interface IBookCatalogueService
    {
        public Task<Book> GetBookByIdAsync(string bookId);
        public Task<List<Book>> GetAllBooksAsync();
        public bool IsAlreadyBorrowed(Book book, string readerId);
        public Task<List<Book>> GetBorrowedBooksByReaderIdAsync(string id);
        public Task<List<Book>> GetReturnedBooksByReaderIdAsync(string id);
        public Task<List<Book>> GetHistoryBooksByReaderIdAsync(string id);
        bool IsCurrentlyBorrowed(Book book, string id);
    }
}
