using TVShowTracker.API.Models;

namespace TVShowTracker.API.Repositories
{
    // This interface defines the contract for a repository that handles operations related to a user's favorite TV shows.
    public interface IFavoriteRepository
    {
        Task AddFavoriteAsync(FavoriteShow favorite);
        Task RemoveFavoriteAsync(int userId, int tvShowId);
        Task<bool> IsFavoriteAsync(int userId, int tvShowId);
        Task<IEnumerable<TVShow>> GetFavoritesByUserAsync(int userId);
    }
}
