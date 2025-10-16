using Moq;
using AutoMapper;
using TVShowTracker.API.Services;
using TVShowTracker.API.Repositories;
using TVShowTracker.API.DTOs;
using TVShowTracker.API.Models;


namespace TVShowTracker.Tests
{
    public class RecommendationServiceTests
    {
        private readonly RecommendationService _service;
        private readonly Mock<IFavoriteRepository> _favoriteRepo = new();
        private readonly Mock<ITVShowRepository> _tvShowRepo = new();
        private readonly IMapper _mapper;

        public RecommendationServiceTests()
        {
            // Configuração mínima do AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TVShow, TVShowListDto>();
            });
            _mapper = config.CreateMapper();

            _service = new RecommendationService(_favoriteRepo.Object, _tvShowRepo.Object, _mapper);
        }

        [Fact]
        public async Task GetRecommendationsAsync_ReturnsEmpty_WhenUserHasNoFavorites()
        {
            // Arrange
            _favoriteRepo.Setup(r => r.GetFavoritesByUserAsync(It.IsAny<int>()))
                         .ReturnsAsync(new List<TVShow>());

            // Act
            var result = await _service.GetRecommendationsAsync(1);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetRecommendationsAsync_ReturnsRecommendations_WhenUserHasFavorites()
        {
            // Arrange
            var favoriteShows = new List<TVShow>
            {
                new TVShow { Id = 1, Title = "Favorite Show", Genre = "Drama" }
            };

            _favoriteRepo.Setup(r => r.GetFavoritesByUserAsync(It.IsAny<int>()))
                         .ReturnsAsync(favoriteShows);

            var allShows = new List<TVShow>
            {
                new TVShow { Id = 1, Title = "Favorite Show", Genre = "Drama" }, // já favorito
                new TVShow { Id = 2, Title = "New Drama Show", Genre = "Drama" },
                new TVShow { Id = 3, Title = "Comedy Show", Genre = "Comedy" }
            };

            _tvShowRepo.Setup(r => r.GetPagedAsync(
                    null,       // genre
                    null,       // type
                    null,       // search
                    "releasedate", // sortBy
                    1,          // page
                    int.MaxValue // pageSize
                ))
                .ReturnsAsync((allShows, allShows.Count));

            // Act
            var result = await _service.GetRecommendationsAsync(1);

            // Assert
            var recommendedTitles = result.Select(r => r.Title).ToList();
            Assert.Single(recommendedTitles);
            Assert.Contains("New Drama Show", recommendedTitles);
            Assert.DoesNotContain("Favorite Show", recommendedTitles); // não recomenda já favorito
        }
    }
}
