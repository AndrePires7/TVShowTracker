using Microsoft.EntityFrameworkCore;
using TVShowTracker.API.Data;
using TVShowTracker.API.Models;
using TVShowTracker.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    //Get user by ID with favorites included
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.FavoriteShows)
            .ThenInclude(f => f.TVShow)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    //Delete user from database
    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
