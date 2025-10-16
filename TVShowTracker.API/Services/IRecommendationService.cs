using TVShowTracker.API.DTOs;

namespace TVShowTracker.API.Services
{
    //Interface defining the contract for the Recommendation Service.
    public interface IRecommendationService
    {
        Task<IEnumerable<TVShowListDto>> GetRecommendationsAsync(int userId, int maxResults = 5);
    }
}
