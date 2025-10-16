namespace TVShowTracker.API.DTOs
{
    // This class is designed for listing TVShows in the API responses without exposing the full internal model.
    public class TVShowListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTimeOffset ReleaseDate { get; set; }
        public string? ImageUrl { get; set; }
    }
}
