namespace TVShowTracker.API.DTO
{
    // This class ensures that only relevant information is returned and sensitive data (like password) is not exposed.
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
