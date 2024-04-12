
using LIbrary.Repository.Specific;

namespace LIbrary.Services.Reminder
{
    public class ReminderService : IReminderService
    {
        private readonly IBorrowItemRepository _borrowItemRepository;
        public ReminderService(IBorrowItemRepository borrowItemRepository)
        {
            _borrowItemRepository = borrowItemRepository;
        }
        public Task SendEmails()
        {
            throw new NotImplementedException();
        }
    }
}
