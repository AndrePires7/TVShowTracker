using TVShowTracker.API.DTOs;

namespace TVShowTracker.API.Services
{
    //This interface defines the contract for the Actor service.
    public interface IActorService
    {
        Task<PagedResult<ActorDto>> GetPagedAsync(string? sortBy, int page, int pageSize);
        Task<ActorDto?> GetByIdAsync(int id);
    }
}
