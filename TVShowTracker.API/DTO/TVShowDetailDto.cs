namespace TVShowTracker.API.DTOs
{
    //This class is used to send TVShows from the API to the client.
    public class TVShowDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTimeOffset ReleaseDate { get; set; }
        public string? ImageUrl { get; set; }

        //Collection of episodes for a TV show.
        public IEnumerable<EpisodeDto> Episodes { get; set; } = Enumerable.Empty<EpisodeDto>();

        //Collection of actors featured in this TV show
        public IEnumerable<ActorDto> FeaturedActors { get; set; } = Enumerable.Empty<ActorDto>();
    }
}
