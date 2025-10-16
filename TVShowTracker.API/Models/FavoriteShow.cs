namespace TVShowTracker.API.Models
{

    //Join table representing a many-to-many relationship between User and TVShow (Favotires)
    public class FavoriteShow
    {

        //Foreign key to User
        public int UserId { get; set; }
        public User? User { get; set; }

        //Foreign key to TVShow
        public int TVShowId { get; set; }
        public TVShow? TVShow { get; set; }

        //Record when it was favorited
        public DateTimeOffset AddedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
