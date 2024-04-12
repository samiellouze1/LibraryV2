
using LIbrary.Repository.Specific;

namespace LIbrary.Services.Reminder
{
    public class BackgroundRefresh : IHostedService, IDisposable
    {
        private readonly IReminderService _reminderService;
        private Timer? _timer;

        public BackgroundRefresh(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer( SomeMethod, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            //_timer = new Timer( SomeMethod, null, TimeSpan.Zero, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        private async void SomeMethod(object? state)
        {
            Console.WriteLine("ziw ziw");
            //_reminderService.SendEmails().Wait();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
