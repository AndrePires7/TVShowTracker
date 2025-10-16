namespace TVShowTracker.API.Services
{
    // This interface defines a contract for sending emails in the application.
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
