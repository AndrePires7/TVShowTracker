using System.ComponentModel.DataAnnotations;

namespace TVShowTracker.API.Models
{

    // Represents a TV show or series, with related actors, episodes, and favorites.
    public class TVShow
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Genre { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Type { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; }

        public string? ImageUrl { get; set; } = string.Empty;

        //Many-to-many relationship linking this TV show to its featured actors
        public ICollection<ShowActor> ShowActors { get; set; } = new List<ShowActor>();


        //Many-to-many relationship linking this TV show to users who marked it as favorite
        public ICollection<FavoriteShow> FavoriteShows { get; set; } = new List<FavoriteShow>();

        //One-to-many relationship: all episodes belonging to this TV show
        public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    }
}
