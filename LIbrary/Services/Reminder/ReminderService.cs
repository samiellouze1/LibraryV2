using LIbrary.Repository.Specific;
using LIbrary.Services.EmailSender;

namespace LIbrary.Services.Reminder
{
    public class ReminderService : IReminderService
    {
        private readonly IBorrowItemRepository _borrowItemRepository;
        private readonly IEmailSender _emailSender;
        public ReminderService(IBorrowItemRepository borrowItemRepository, IEmailSender emailSender)
        {
            _borrowItemRepository = borrowItemRepository;
            _emailSender = emailSender;
        }
        public async Task SendEmails()
        {
            var borrowitems = await _borrowItemRepository.GetAllEagerBorrowItems();
            foreach (var borrowitem in borrowitems)
            {
                if (borrowitem.borrowItemStatusId=="1" && (DateTime.Now - borrowitem.supposedEndDate).Days<1)
                {
                    var reader = borrowitem.reader;
                    await _emailSender.SendEmailAsync(reader.UserName, "Reminder to return book" + borrowitem.bookCopy.book.title, "You need to return your book in less than one day");
                }
            }
            Console.WriteLine("ziw ziw");
        }
    }
}
