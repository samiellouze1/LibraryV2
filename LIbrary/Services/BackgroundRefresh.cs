using LIbrary.Repository.Specific;
using LIbrary.Services.Reminder;

namespace LIbrary.Services
{
    public class BackgroundRefresh : BackgroundService
    {
        private readonly IServiceProvider _services;

        public BackgroundRefresh(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var scopedService = scope.ServiceProvider.GetRequiredService<IReminderService>();
                    await scopedService.SendEmails();
                }
                await Task.Delay(TimeSpan.FromHours(2), stoppingToken);
            }
        }
    }
}
