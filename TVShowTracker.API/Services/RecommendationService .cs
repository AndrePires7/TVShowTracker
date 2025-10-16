using AutoMapper;
using TVShowTracker.API.DTOs;
using TVShowTracker.API.Repositories;

namespace TVShowTracker.API.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IFavoriteRepository _favoriteRepo;
        private readonly ITVShowRepository _tvShowRepo;
        private readonly IMapper _mapper;

        public RecommendationService(IFavoriteRepository favoriteRepo, ITVShowRepository tvShowRepo, IMapper mapper)
        {
            _favoriteRepo = favoriteRepo;
            _tvShowRepo = tvShowRepo;
            _mapper = mapper;
        }

        //Generates personalized TV show recommendations for a user based on their favorite shows.
        public async Task<IEnumerable<TVShowListDto>> GetRecommendationsAsync(int userId, int maxResults = 5)
        {
            //Fetch the user's favorite TV shows
            var favorites = await _favoriteRepo.GetFavoritesByUserAsync(userId);
            if (!favorites.Any())
                return Enumerable.Empty<TVShowListDto>();

            //Extract distinct genres from the user's favorites
            var favoriteGenres = favorites.Select(f => f.Genre).Distinct().ToList();

            //Fetch all TV shows from the database
            var (allShows, _) = await _tvShowRepo.GetPagedAsync(
                genre: null,
                type: null,
                search: null,
                sortBy: "releasedate",
                page: 1,
                pageSize: int.MaxValue); //Get all shows to filter manually

            //Filter shows to include only ones matching user's favorite genres and exclude shows already marked as favorite
            var recommended = allShows
                .Where(s => favoriteGenres.Contains(s.Genre) && !favorites.Any(f => f.Id == s.Id))
                .Take(maxResults)
                .ToList();

            //Map entities to DTOs for API response
            return recommended.Select(s => _mapper.Map<TVShowListDto>(s));
        }
    }
}
