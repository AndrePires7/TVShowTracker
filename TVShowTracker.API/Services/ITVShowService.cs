using TVShowTracker.API.DTOs;

namespace TVShowTracker.API.Services
{
    //Service interface defines what operations can be done on TV Shows
    public interface ITVShowService
    {
        //Returns a paged list of TV Shows with optional filtering and sorting
        Task<PagedResult<TVShowListDto>> GetPagedAsync(string? genre, string? type, string? search, string? sortBy, int page, int pageSize);

        //Returns detailed info of a single TV Show including episodes and featured actors
        Task<TVShowDetailDto?> GetByIdAsync(int id);

        Task<IEnumerable<string>> GetGenresAsync();
        Task<IEnumerable<string>> GetTypesAsync();
    }
}
