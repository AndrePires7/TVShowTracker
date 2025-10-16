    using Microsoft.EntityFrameworkCore;
    using TVShowTracker.API.Data;
    using TVShowTracker.API.Helpers;
    using TVShowTracker.API.Models;

    namespace TVShowTracker.API.Repositories
    {
    //Repository responsible for all database operations related to Actor entity
    //Encapsulates EF Core queries and ensures separation of data access logic
    public class ActorRepository : IActorRepository
    {
        private readonly AppDbContext _db;

        //Constructor receives the DbContext via Dependency Injection
        public ActorRepository(AppDbContext db) => _db = db;

        //Retrieves a paged list of actors, ordered by name
        //Supports pagination using the ApplyPagingAsync helper
        //Returns a tuple: IEnumerable<Actor> and total count
        public async Task<(IEnumerable<Actor> Items, long Total)> GetPagedAsync(string? sortBy, int page, int pageSize)
        {
            var query = _db.Actors
                .Include(a => a.ShowActors)
                .ThenInclude(ta => ta.TVShow)
                .AsQueryable();

            //Sort dynamically by name
            query = sortBy?.ToLower() switch
            {
                "name" => query.OrderBy(a => a.Name),
                "namedsc" => query.OrderByDescending(a => a.Name),
                _ => query.OrderBy(a => a.Name) // default
            };

            //Apply pagination using the same helper as for TVShows
            return await query.ApplyPagingAsync(page, pageSize);
        }

        //Retrieves a single Actor by ID, including all TV shows they appeared in
        //Uses EF Core Include/ThenInclude to load related entities
        public async Task<Actor?> GetByIdWithShowsAsync(int id)
        {
            return await _db.Actors
                .Include(a => a.ShowActors)             //Load the join table for TVShows
                .ThenInclude(sa => sa.TVShow)           //Load each related TVShow entity
                .FirstOrDefaultAsync(a => a.Id == id);  //Filter actor by ID
        }
    }
    }
