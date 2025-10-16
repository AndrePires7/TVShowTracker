using System.ComponentModel.DataAnnotations;

namespace TVShowTracker.API.Models
{

    //Represents an actor appearing in one or more TV shows.
    public class Actor
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Bio { get; set; } = string.Empty;

        [Url]
        public string? ImageUrl { get; set; } = string.Empty;

        //Shows the actor has appeared in.
        public ICollection<ShowActor> ShowActors { get; set; } = new List<ShowActor>();
    }
}
