namespace TVShowTracker.API.DTOs
{
    //This class defines only the minimal information required from the client to perform the "add to favorites" operation.
    public class CreateFavoriteDto
    {
        public int TvShowId { get; set; }
    }
}
