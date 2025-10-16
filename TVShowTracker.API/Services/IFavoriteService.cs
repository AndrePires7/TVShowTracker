using TVShowTracker.API.DTOs;

namespace TVShowTracker.API.Services
{
    //This interface defines the contract for managing user favorite TV shows.
    public interface IFavoriteService
    {
        Task AddFavoriteAsync(int userId, int tvShowId);
        Task RemoveFavoriteAsync(int userId, int tvShowId);
        Task<IEnumerable<TVShowListDto>> GetFavoritesAsync(int userId);
    }
}
