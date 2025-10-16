using TVShowTracker.API.Models;

namespace TVShowTracker.API.Services
{
    //This interface is responsible for providing functionality to export user's information.
    public interface IExportService
    {
        Task<byte[]> ExportUserFavoritesToCsvAsync(User user);
        Task<byte[]> ExportUserFavoritesToPdfAsync(User user);
    }
}
