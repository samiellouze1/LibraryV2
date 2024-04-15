using LIbrary.Repository.Specific;
using LIbrary.ViewModels.ReturnBook;
using LIbrary.Services.ReturnBook;
using LIbrary.Models;

namespace LIbrary.Services.ReturnBook
{
    public class ReturnBookService : IReturnBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowItemStatusRepository _borrowItemStatusRepository;
        private readonly IBorrowItemRepository _borrowItemRepository;
        private readonly IFineStatusRepository _fineStatusRepository;

        public ReturnBookService(IBookRepository bookRepository, IBorrowItemStatusRepository borrowItemStatusRepository, IBorrowItemRepository borrowItemRepository, IFineStatusRepository fineStatusRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _borrowItemStatusRepository = borrowItemStatusRepository ?? throw new ArgumentNullException(nameof(borrowItemStatusRepository));
            _borrowItemRepository = borrowItemRepository ?? throw new ArgumentNullException(nameof(borrowItemRepository));
            _fineStatusRepository = fineStatusRepository;
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

            var borrowItems = book.bookCopies.SelectMany(bc => bc.borrowItems).Where(bi => bi.borrowItemStatusId == "1");
            var borrowItem = borrowItems.FirstOrDefault(bi => bi.readerId == readerId);
            if (borrowItem != null)
            {
                var returnDate = DateTime.Now;
                var returnedBorrowItemStatus = await _borrowItemStatusRepository.GetByIdAsync("2");
                borrowItem.endDate = returnDate;
                borrowItem.borrowItemStatus = returnedBorrowItemStatus;
                if (borrowItem.supposedEndDate<borrowItem.endDate)
                {
                    FineStatus NotPaidFineStatus = await _fineStatusRepository.GetByIdAsync("1");
                    Fine fine = new Fine() { fineStatus = NotPaidFineStatus };
                    borrowItem.fine = fine;
                }
                borrowItem.reviewRating = new Models.ReviewRating() { rating=returnBookVM.rating,review=returnBookVM.review };
                await _borrowItemRepository.UpdateAsync(borrowItem.Id, borrowItem);
            }
            else
            {
                throw new InvalidOperationException("You didn't borrow this book.");
            }
        }
    }
}
