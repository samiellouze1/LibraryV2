using LIbrary.Models;
using LIbrary.Repository.Specific;
using LIbrary.Services.BookCatalogue;

namespace LIbrary.Services.BookCatalogue
{
    public class BookCatalogueService : IBookCatalogueService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookCopyRepository _bookCopyRepository;
        private readonly IBorrowItemRepository _borrowItemRepository;
        private readonly IReaderRepository _readerRepository;

        public BookCatalogueService(IBookRepository bookRepository, IBookCopyRepository bookCopyRepository, IBorrowItemRepository borrowItemRepository, IReaderRepository readerRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _bookCopyRepository = bookCopyRepository ?? throw new ArgumentNullException(nameof(bookCopyRepository));
            _borrowItemRepository = borrowItemRepository ?? throw new ArgumentNullException(nameof(borrowItemRepository));
            _readerRepository = readerRepository ?? throw new ArgumentNullException(nameof(readerRepository));
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllEagerBooksAsync() ?? new List<Book>();
            return books.ToList();
        }

        public async Task<Book> GetBookByIdAsync(string bookId)
        {
            if (string.IsNullOrEmpty(bookId))
                throw new ArgumentException("Book ID cannot be null or empty", nameof(bookId));

            var book = await _bookRepository.GetEagerBookByIdAsync(bookId);
            return book;
        }

        public bool IsAlreadyBorrowed(Book book, string readerId)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            var readerIds = book.bookCopies
                .SelectMany(bc => bc.borrowItems)
                .Where(bi => bi.borrowItemStatusId == "1")
                .Select(bi => bi.readerId)
                .ToList();

            return readerIds.Contains(readerId);
        }

        public async Task<List<Book>> GetBorrowedBooksByReaderIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Reader ID cannot be null or empty", nameof(id));

            var reader = await _readerRepository.GetEagerReaderByIdAsync(id);
            var books = reader?.borrowItems
                .Where(bi => bi.borrowItemStatusId == "1")
                .Select(r => r.bookCopy?.book)
                .ToList() ?? new List<Book>();

            return books;
        }

        public async Task<List<Book>> GetHistoryBooksByReaderIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Reader ID cannot be null or empty", nameof(id));

            var reader = await _readerRepository.GetEagerReaderByIdAsync(id);
            var books = reader?.borrowItems
                .Select(r => r.bookCopy?.book)
                .ToList() ?? new List<Book>();

            return books;
        }

        public async Task<List<Book>> GetReturnedBooksByReaderIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Reader ID cannot be null or empty", nameof(id));

            var reader = await _readerRepository.GetEagerReaderByIdAsync(id);
            var books = reader?.borrowItems
                .Where(bi => bi.borrowItemStatusId == "2")
                .Select(r => r.bookCopy?.book)
                .ToList() ?? new List<Book>();

            return books;
        }
    }
}
