using LIbrary.Models;

namespace LIbrary.Services.BookCatalogue
{
    public interface IBookCatalogueService
    {
        public Task<Book> GetBookByIdAsync(string bookId);
        public Task<List<Book>> GetAllBooksAsync();
        public Task<List<Book>> GetBorrowedBooksByReaderIdAsync(string id);
        public Task<List<Book>> GetReturnedBooksByReaderIdAsync(string id);
        bool IsCurrentlyBorrowed(Book book, string id);
        public bool IsAlreadyBorrowed(Book book, string readerId);
    }
}
