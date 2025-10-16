namespace TVShowTracker.API.DTOs
{
    // This class is used to transfer episode data between backend and frontend without exposing the internal database model directly.
    public class EpisodeDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }
        public string? Synopsis { get; set; }
    }
}
