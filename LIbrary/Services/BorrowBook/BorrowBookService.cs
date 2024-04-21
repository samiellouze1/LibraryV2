using LIbrary.Models;
using LIbrary.Repository.Specific;
using LIbrary.ViewModels.BorrowBook;
using LIbrary.Services.ReturnBook;

namespace LIbrary.Services.ReturnBook
{
    public class BorrowBookService : IBorrowBookService
    {
        private readonly IReaderRepository _readerRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowItemStatusRepository _borrowItemStatusRepository;
        private readonly IBorrowItemRepository _borrowItemRepository;

        public BorrowBookService(IReaderRepository readerRepository, IBookRepository bookRepository, IBorrowItemStatusRepository borrowItemStatusRepository, IBorrowItemRepository borrowItemRepository)
        {
            _readerRepository = readerRepository ?? throw new ArgumentNullException(nameof(readerRepository));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _borrowItemStatusRepository = borrowItemStatusRepository ?? throw new ArgumentNullException(nameof(borrowItemStatusRepository));
            _borrowItemRepository = borrowItemRepository ?? throw new ArgumentNullException(nameof(borrowItemRepository));
        }

        public async Task BorrowBook(BorrowBookVM borrowBookVM, string readerId)
        {
            if (string.IsNullOrEmpty(readerId))
                throw new ArgumentException("Reader ID cannot be null or empty.", nameof(readerId));

            if (borrowBookVM == null)
                throw new ArgumentNullException(nameof(borrowBookVM));

            Reader reader = await _readerRepository.GetByIdAsync(readerId);
            if (reader == null)
                throw new InvalidOperationException($"Reader with ID '{readerId}' not found.");

            Book book = await _bookRepository.GetEagerBookByIdAsync(borrowBookVM.bookReadVM.Id);
            if (book == null)
                throw new InvalidOperationException($"Book with ID '{borrowBookVM.bookReadVM.Id}' not found.");

            BookCopy bookCopy = book.bookCopies.FirstOrDefault(bc => !bc.borrowItems.Any(bi => bi.borrowItemStatusId == "1"));
            if (bookCopy == null)
                throw new Exception("No available copies of the book.");

            BorrowItemStatus borrowedStatus = await _borrowItemStatusRepository.GetByIdAsync("1");
            if (borrowedStatus == null)
                throw new InvalidOperationException("Borrow item status '1' not found.");

            BorrowItem borrowItem = new BorrowItem()
            {
                bookCopy = bookCopy,
                reader = reader,
                borrowItemStatus = borrowedStatus,
                supposedEndDate = borrowBookVM.EndDate,
                startDate = borrowBookVM.StartDate
            };

            await _borrowItemRepository.AddAsync(borrowItem);
        }
    }
}
