using System.ComponentModel.DataAnnotations;

namespace TVShowTracker.API.Models

{

    //Represents an episode of a TV show
    public class Episode
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; } = string.Empty;

        public int SeasonNumber { get; set; }

        public int EpisodeNumber { get; set; }

        public DateTime ReleaseDate { get; set; }

        [MaxLength(4000)]
        public string Synopsis { get; set; } = string.Empty;

        //Foreign key to parent TVShow
        public int TVShowId { get; set; }

        public TVShow? TVShow { get; set; }
    }
}
