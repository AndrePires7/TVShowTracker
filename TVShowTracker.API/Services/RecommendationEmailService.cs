using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using TVShowTracker.API.Data;
using TVShowTracker.API.Repositories;
using TVShowTracker.API.Services;

//Background service that periodically sends TV show recommendations to users via email
public class RecommendationEmailService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RecommendationEmailService> _logger;

    public RecommendationEmailService(IServiceProvider serviceProvider, ILogger<RecommendationEmailService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    //Main execution loop of the background service
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Iniciando envio de recomendações por email...");

            try
            {
                //Create a scoped service provider to resolve scoped services like DbContext
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var recommendationService = scope.ServiceProvider.GetRequiredService<IRecommendationService>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                //Retrieve all users from the database
                var users = await dbContext.Users.ToListAsync();

                foreach (var user in users)
                {
                    //Get personalized TV show recommendations for the current user
                    var recommendations = await recommendationService.GetRecommendationsAsync(user.Id);

                    if (!recommendations.Any())
                        continue; //Skip users with no recommendations

                    //Build the email body using HTML
                    var body = new StringBuilder();
                    body.Append("<h3>As tuas recomendações de hoje</h3>");
                    body.Append("<ul>");
                    foreach (var show in recommendations)
                    {
                        body.Append($"<li><b>{show.Title}</b> - {show.Genre}</li>");
                    }
                    body.Append("</ul>");

                    //Send the email to the user
                    await emailService.SendEmailAsync(user.Email, "Novas Recomendações de Séries", body.ToString());
                    _logger.LogInformation($"Email enviado para {user.Email}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar emails de recomendação.");
            }

            //Wait 24 hours before running the loop again
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}
