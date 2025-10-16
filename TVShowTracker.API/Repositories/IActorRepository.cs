using TVShowTracker.API.Models;

namespace TVShowTracker.API.Repositories
{
    //This interface defines the contract for Actor-related database operations.
    public interface IActorRepository
    {
        Task<(IEnumerable<Actor> Items, long Total)> GetPagedAsync(string? sorBy, int page, int pageSize);
        Task<Actor?> GetByIdWithShowsAsync(int id);
    }
}
