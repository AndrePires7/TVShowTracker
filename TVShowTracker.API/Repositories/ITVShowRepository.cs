using Microsoft.AspNetCore.Http.HttpResults;
using TVShowTracker.API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TVShowTracker.API.Repositories
{
    // This interface defines the contract for all operations related to TVShow data.
    public interface ITVShowRepository
    {
        Task<(IEnumerable<TVShow> Items, long Total)> GetPagedAsync(string? genre, string? type, string? search, string? sortBy, int page, int pageSize);
        Task<TVShow?> GetByIdWithEpisodesAndActorsAsync(int id);
        Task AddAsync(TVShow show);
        Task UpdateAsync(TVShow show);
        Task DeleteAsync(TVShow show);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<string>> GetDistinctGenresAsync();
        Task<IEnumerable<string>> GetDistinctTypesAsync();
    }
}
