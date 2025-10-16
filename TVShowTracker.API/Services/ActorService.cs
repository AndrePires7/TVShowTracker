using AutoMapper;
using TVShowTracker.API.DTOs;
using TVShowTracker.API.Repositories;

namespace TVShowTracker.API.Services
{
    //Service layer for Actor-related business logic
    public class ActorService : IActorService
    {
        private readonly IActorRepository _repo; //Repository to access Actor data
        private readonly IMapper _mapper; //AutoMapper to map Entities <-> DTOs

        //Constructor injection for DI
        public ActorService(IActorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //Get a single actor by Id including the shows they've appeared in
        public async Task<ActorDto?> GetByIdAsync(int id)
        {
            //Fetch actor entity with related TVShows from repository
            var entity = await _repo.GetByIdWithShowsAsync(id);
            if (entity == null) return null;

            //Map Actor entity to ActorDto
            var dto = _mapper.Map<ActorDto>(entity);
            return dto;
        }

        //Get paginated list of actors
        public async Task<PagedResult<ActorDto>> GetPagedAsync(string? sortBy, int page, int pageSize)
        {
            //Fetch paged result from repository
            var (items, total) = await _repo.GetPagedAsync(sortBy, page, pageSize);

            //Map each Actor entity to ActorDto
            var dtos = items.Select(a => _mapper.Map<ActorDto>(a)).ToList();

            //Return results in a standard PagedResult format
            return new PagedResult<ActorDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = dtos
            };
        }
    }
}
