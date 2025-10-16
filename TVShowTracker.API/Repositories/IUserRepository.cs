using TVShowTracker.API.Models;

namespace TVShowTracker.API.Repositories
{
    //This interface defines the contract for a repository that manages User entities in the database.
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task DeleteAsync(User user);

    }
}
