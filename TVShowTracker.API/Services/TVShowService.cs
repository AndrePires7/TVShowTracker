using AutoMapper;
using Org.BouncyCastle.Crypto;
using TVShowTracker.API.DTOs;
using TVShowTracker.API.Repositories;

namespace TVShowTracker.API.Services
{
    //Implementation of ITVShowService
    public class TVShowService : ITVShowService
    {
        private readonly ITVShowRepository _repo; //Repository to access DB
        private readonly IMapper _mapper; //AutoMapper to convert Entities <-> DTOs
        private readonly ICacheService _cacheService;

        public TVShowService(ITVShowRepository repo, IMapper mapper, ICacheService cacheService)
        {
            _repo = repo;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        //Returns detailed info of a TV show including episodes and actors
        public async Task<TVShowDetailDto?> GetByIdAsync(int id)
        {
            //Get entity with related data from repository
            var entity = await _repo.GetByIdWithEpisodesAndActorsAsync(id);
            if (entity == null) return null;

            //Map entity to DTO
            var dto = _mapper.Map<TVShowDetailDto>(entity);

            //Map featured actors separately (from join table ShowActors)
            dto.FeaturedActors = entity.ShowActors
                .Select(sa => _mapper.Map<ActorDto>(sa.Actor))
                .ToList();

            return dto;
        }

        //Returns a paged list of TV Shows with optional filters and sorting
        public async Task<PagedResult<TVShowListDto>> GetPagedAsync(string? genre, string? type, string? search, string? sortBy, int page, int pageSize)
        {
            //Ask repository for filtered, sorted, paginated data
            var (items, total) = await _repo.GetPagedAsync(genre, type, search, sortBy, page, pageSize);

            //Map each TVShow entity to TVShowListDto
            var dtos = items.Select(s => _mapper.Map<TVShowListDto>(s)).ToList();

            //Return paged result
            return new PagedResult<TVShowListDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = dtos
            };
        }

        //Get all genres with cache
        public async Task<IEnumerable<string>> GetGenresAsync()
        {
            return await _cacheService.GetOrAddAsync("genres", async () =>
                await _repo.GetDistinctGenresAsync(),
                TimeSpan.FromSeconds(10));
        }

        //Get all types with cache
        public async Task<IEnumerable<string>> GetTypesAsync()
        {
            return await _cacheService.GetOrAddAsync("types", async () =>
                await _repo.GetDistinctTypesAsync(),
                TimeSpan.FromSeconds(10));
        }

    }
}
