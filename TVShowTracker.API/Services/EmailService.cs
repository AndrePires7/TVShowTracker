using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using TVShowTracker.API.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    //Sends an email asynchronously using the SMTP configuration
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        //Configure the SMTP client using settings from configuration
        var smtpClient = new SmtpClient(_config["Smtp:Host"])
        {
            Port = int.Parse(_config["Smtp:Port"]!),
            Credentials = new NetworkCredential(_config["Smtp:User"], _config["Smtp:Pass"]),
            EnableSsl = true
        };

        //Create the email message
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_config["Smtp:From"]!),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(to);
        await smtpClient.SendMailAsync(mailMessage);
    }
}
