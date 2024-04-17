using LIbrary.Repository.Specific;
using LIbrary.Services.Reminder;

namespace LIbrary.Services
{
    public class BackgroundRefresh : IHostedService, IDisposable
    {
        private Timer? _timer;
        private readonly IServiceProvider _serviceProvider;

        public BackgroundRefresh(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<IReminderService>();
                _timer = new Timer(state => scopedService.SendEmails().Wait(), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
                return Task.CompletedTask;
            }
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
