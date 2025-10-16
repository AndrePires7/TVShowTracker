using Microsoft.EntityFrameworkCore;
using TVShowTracker.API.Data;
using TVShowTracker.API.Models;

namespace TVShowTracker.API.Repositories
{
    //Repository responsible for managing user favorite TV shows
    //Handles adding, removing, checking, and retrieving favorites from the database
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AppDbContext _db;

        //Constructor receives the DbContext via Dependency Injection
        public FavoriteRepository(AppDbContext db) => _db = db;

        //Adds a new favorite TV show for a user
        public async Task AddFavoriteAsync(FavoriteShow favorite)
        {
            _db.FavoriteShows.Add(favorite);       //Adds entity to DbContext
            await _db.SaveChangesAsync();          //Persists changes to the database
        }

        //Retrieves all favorite TV shows for a given user
        public async Task<IEnumerable<TVShow>> GetFavoritesByUserAsync(int userId)
        {
            return await _db.FavoriteShows
                .Where(f => f.UserId == userId)       // Filter by user
                .Select(f => f.TVShow)                // Return only the TVShow objects
                .ToListAsync();                       // Execute query asynchronously
        }

        //Checks if a given TV show is already a favorite for a user
        public async Task<bool> IsFavoriteAsync(int userId, int tvShowId)
        {
            return await _db.FavoriteShows.AnyAsync(f => f.UserId == userId && f.TVShowId == tvShowId); // Returns true if exists
        }

        //Removes a favorite TV show for a user
        public async Task RemoveFavoriteAsync(int userId, int tvShowId)
        {
            //Find the favorite entity
            var fav = await _db.FavoriteShows.FirstOrDefaultAsync(f => f.UserId == userId && f.TVShowId == tvShowId);

            if (fav != null)
            {
                _db.FavoriteShows.Remove(fav);    //Remove entity from DbContext
                await _db.SaveChangesAsync();     //Persist removal in the database
            }
            //If not found, method does nothing (idempotent)
        }
    }
}
