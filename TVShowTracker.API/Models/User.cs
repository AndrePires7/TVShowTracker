namespace TVShowTracker.API.Models
{
    // Represents a registered user in the system.
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        //Collection of user's favorite TV shows
        public ICollection<FavoriteShow> FavoriteShows { get; set; } = new List<FavoriteShow>();
    }
}
