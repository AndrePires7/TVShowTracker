using Microsoft.EntityFrameworkCore;
using TVShowTracker.API.Data;
using TVShowTracker.API.Helpers;
using TVShowTracker.API.Models;

namespace TVShowTracker.API.Repositories
{
    //Repository to handle all database operations for TVShow entity
    //Encapsulates EF Core queries and CRUD operations
    public class TVShowRepository : ITVShowRepository
    {
        private readonly AppDbContext _db;

        //Constructor receives DbContext via Dependency Injection
        public TVShowRepository(AppDbContext db) => _db = db;

        //Adds a new TVShow to the database
        public async Task AddAsync(TVShow show)
        {
            _db.TVShows.Add(show);
            await _db.SaveChangesAsync();
        }

        //Removes an existing TVShow from the database
        public async Task DeleteAsync(TVShow show)
        {
            _db.TVShows.Remove(show);
            await _db.SaveChangesAsync();
        }

        //Retrieves a TVShow including its Episodes and Featured Actors
        public async Task<TVShow?> GetByIdWithEpisodesAndActorsAsync(int id)
        {
            return await _db.TVShows
                .Include(s => s.Episodes)               //Load related episodes
                .Include(s => s.ShowActors)             //Load join table ShowActors
                .ThenInclude(sa => sa.Actor)            //Load related Actor entities
                .FirstOrDefaultAsync(s => s.Id == id);  //Filter by ID
        }

        //Retrieves TVShows with optional filters, sorting, and pagination
        public async Task<(IEnumerable<TVShow> Items, long Total)> GetPagedAsync(string? genre, string? type, string? search, string? sortBy, int page, int pageSize)
        {
            var query = _db.TVShows.AsQueryable();

            //Filter by genre if provided
            if (!string.IsNullOrWhiteSpace(genre))
                query = query.Where(s => s.Genre == genre);

            //Filter by type if provided
            if (!string.IsNullOrWhiteSpace(type))
                query = query.Where(s => s.Type == type);

            //Filter by search term in title or description
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(s => s.Title.Contains(search) || s.Description.Contains(search));

            query = sortBy?.ToLower() switch
            {
                "title" => query.OrderBy(s => s.Title),
                "titledsc" => query.OrderByDescending(s => s.Title),
                "releasedate" => query.OrderBy(s => s.ReleaseDate),
                "releasedatedsc" => query.OrderByDescending(s => s.ReleaseDate),
                "genre" => query.OrderBy(s => s.Genre),
                "genredsc" => query.OrderByDescending(s => s.Genre),
                "type" => query.OrderBy(s => s.Type),
                "typedsc" => query.OrderByDescending(s => s.Type),
                _ => query.OrderBy(s => s.Title)
            };

            //Apply pagination using helper extension
            return await query.ApplyPagingAsync(page, pageSize);
        }

        //Updates an existing TVShow entity
        public async Task UpdateAsync(TVShow show)
        {
            _db.TVShows.Update(show);
            await _db.SaveChangesAsync();
        }

        //Checks if a TVShow exists by ID
        public async Task<bool> ExistsAsync(int id) =>
            await _db.TVShows.AnyAsync(s => s.Id == id);

        public async Task<IEnumerable<string>> GetDistinctGenresAsync()
        {
            return await _db.TVShows
                .Select(s => s.Genre)
                .Where(g => !string.IsNullOrEmpty(g))
                .Distinct()
                .OrderBy(g => g)
                .ToListAsync();
        }

        //Returns a distinct list of all TV Show types (e.g., Series, Mini-Series, etc.)
        public async Task<IEnumerable<string>> GetDistinctTypesAsync()
        {
            return await _db.TVShows
                .Select(s => s.Type)
                .Where(t => !string.IsNullOrEmpty(t))
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync();
        }
    }
}
