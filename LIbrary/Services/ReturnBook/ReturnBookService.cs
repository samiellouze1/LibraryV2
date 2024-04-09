using LIbrary.Repository.Specific;
using LIbrary.ViewModels.ReturnBook;
using LIbrary.Services.ReturnBook;

namespace LIbrary.Services.ReturnBook
{
    public class ReturnBookService : IReturnBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowItemStatusRepository _borrowItemStatusRepository;
        private readonly IBorrowItemRepository _borrowItemRepository;

        public ReturnBookService(IBookRepository bookRepository, IBorrowItemStatusRepository borrowItemStatusRepository, IBorrowItemRepository borrowItemRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _borrowItemStatusRepository = borrowItemStatusRepository ?? throw new ArgumentNullException(nameof(borrowItemStatusRepository));
            _borrowItemRepository = borrowItemRepository ?? throw new ArgumentNullException(nameof(borrowItemRepository));
        }

        public async Task ReturnBook(ReturnBookVM returnBookVM, string readerId)
        {
            if (returnBookVM == null)
                throw new ArgumentNullException(nameof(returnBookVM));

            if (string.IsNullOrEmpty(readerId))
                throw new ArgumentException("Reader ID cannot be null or empty.", nameof(readerId));

            var book = await _bookRepository.GetEagerBookByIdAsync(returnBookVM.bookReadVM.Id);
            if (book == null)
                throw new InvalidOperationException($"Book with ID '{returnBookVM.bookReadVM.Id}' not found.");

            var borrowItems = book.bookCopies.SelectMany(bc => bc.borrowItems).Where(bi => bi.borrowItemStatusId == "2");
            var borrowItem = borrowItems.FirstOrDefault(bi => bi.readerId == readerId);
            if (borrowItem != null)
            {
                var returnDate = DateTime.Now;
                var returnedBorrowItemStatus = await _borrowItemStatusRepository.GetByIdAsync("2");
                borrowItem.endDate = returnDate;
                borrowItem.borrowItemStatus = returnedBorrowItemStatus;
                borrowItem.reviewRating = returnBookVM.reviewRating;
                await _borrowItemRepository.UpdateAsync(borrowItem.Id, borrowItem);
            }
            else
            {
                throw new InvalidOperationException("You didn't borrow this book.");
            }
        }
    }
}
