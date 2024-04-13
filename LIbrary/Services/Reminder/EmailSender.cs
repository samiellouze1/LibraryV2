
using Microsoft.AspNetCore.Identity.UI.Services;

namespace LIbrary.Services.Reminder
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
