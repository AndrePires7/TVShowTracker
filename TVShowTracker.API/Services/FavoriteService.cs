using AutoMapper;
using TVShowTracker.API.DTOs;
using TVShowTracker.API.Models;
using TVShowTracker.API.Repositories;

namespace TVShowTracker.API.Services
{
    //Service layer for managing user favorite TV shows
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _repo; //Repository to access FavoriteShow table
        private readonly ITVShowRepository _tvshowRepo; //Repository to check if TVShow exists
        private readonly IMapper _mapper; //AutoMapper to map TVShow entities to DTOs

        //Constructor injection for DI
        public FavoriteService(IFavoriteRepository repo, ITVShowRepository tvshowRepo, IMapper mapper)
        {
            _repo = repo;
            _tvshowRepo = tvshowRepo;
            _mapper = mapper;
        }

        //Add a TV show to user's favorites
        public async Task AddFavoriteAsync(int userId, int tvShowId)
        {
            //Business rule: check if the TV show exists
            if (!await _tvshowRepo.ExistsAsync(tvShowId))
                throw new Exception("TV Show not found");

            //Idempotency: do nothing if already favorited
            if (await _repo.IsFavoriteAsync(userId, tvShowId))
                return;

            //Create FavoriteShow entity and save
            var fav = new FavoriteShow { UserId = userId, TVShowId = tvShowId };
            await _repo.AddFavoriteAsync(fav);
        }

        //Get all favorites for a user
        public async Task<IEnumerable<TVShowListDto>> GetFavoritesAsync(int userId)
        {
            //Get TVShow entities from repository
            var shows = await _repo.GetFavoritesByUserAsync(userId);

            //Map each TVShow entity to DTO
            return shows.Select(s => _mapper.Map<TVShowListDto>(s));
        }

        //Remove a TV show from user's favorites
        public async Task RemoveFavoriteAsync(int userId, int tvShowId)
        {
            await _repo.RemoveFavoriteAsync(userId, tvShowId);
        }
    }
}
