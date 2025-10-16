namespace TVShowTracker.API.DTOs
{
    //This class is used to send actor data to clients without exposing the full database model.
    public class ActorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }

        //Lista de TV Shows em que o ator apareceu
        public List<TVShowListDto> TVShows { get; set; } = new();
    }
}
