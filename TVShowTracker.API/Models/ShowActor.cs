namespace TVShowTracker.API.Models
{

    //Join table representing a many-to-many relationship between TVShow and Actor
    public class ShowActor
    {

        //Foreign key to TVShow
        public int TVShowId { get; set; }

        public TVShow? TVShow { get; set; }

        // Foreign key to Actor
        public int ActorId { get; set; }

        public Actor? Actor { get; set; }
    }
}
