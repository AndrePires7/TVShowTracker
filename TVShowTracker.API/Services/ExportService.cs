using CsvHelper;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Globalization;
using System.IO;
using TVShowTracker.API.Models;
using TVShowTracker.API.Services;

public class ExportService : IExportService
{
    //Exports the user info into a csv file
    public async Task<byte[]> ExportUserFavoritesToCsvAsync(User user)
    {
        using var memoryStream = new MemoryStream();
        using var writer = new StreamWriter(memoryStream);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.WriteField("Username");
        csv.WriteField("Email");

        //Only add TVShow columns if there are favorites
        bool hasFavorites = user.FavoriteShows != null && user.FavoriteShows.Any();
        if (hasFavorites)
        {
            csv.WriteField("Title");
            csv.WriteField("Genre");
            csv.WriteField("Type");
            csv.WriteField("Description");
        }

        await csv.NextRecordAsync();

        if (hasFavorites)
        {
            bool first = true;
            foreach (var fav in user.FavoriteShows)
            {
                if (first)
                {
                    csv.WriteField(user.Name);
                    csv.WriteField(user.Email);
                    first = false;
                }
                else
                {
                    csv.WriteField("");
                    csv.WriteField("");
                }

                csv.WriteField(fav.TVShow.Title);
                csv.WriteField(fav.TVShow.Genre);
                csv.WriteField(fav.TVShow.Type);
                csv.WriteField(fav.TVShow.Description);
                await csv.NextRecordAsync();
            }
        }
        else
        {
            // Without favorites: only Username e Email
            csv.WriteField(user.Name);
            csv.WriteField(user.Email);
            await csv.NextRecordAsync();
        }

        await writer.FlushAsync();
        return memoryStream.ToArray();
    }

    //Exports the user info into a pdf file
    public Task<byte[]> ExportUserFavoritesToPdfAsync(User user)
    {
        var pdfBytes = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(20);

                page.Header().Text($"User: {user.Name} - Email: {user.Email}")
                    .SemiBold().FontSize(18);

                //Only add TVShow columns if there are favorites
                if (user.FavoriteShows != null && user.FavoriteShows.Any())
                {
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Title").Bold();
                            header.Cell().Text("Genre").Bold();
                            header.Cell().Text("Type").Bold();
                            header.Cell().Text("Description").Bold();
                        });

                        foreach (var fav in user.FavoriteShows)
                        {
                            table.Cell().Text(fav.TVShow.Title);
                            table.Cell().Text(fav.TVShow.Genre);
                            table.Cell().Text(fav.TVShow.Type);
                            table.Cell().Text(fav.TVShow.Description);
                        }
                    });
                }
                else
                {
                    page.Content().Text("No favorite TV shows found.");
                }
            });
        }).GeneratePdf();

        return Task.FromResult(pdfBytes);
    }
}
