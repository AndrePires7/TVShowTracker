using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TVShowTracker.API.DTOs;
using TVShowTracker.API.Services;
using System.Security.Claims;

namespace TVShowTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Todos endpoints exigem autenticação
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _service;

        public FavoritesController(IFavoriteService service)
        {
            _service = service;
        }

        //Add a TV show to user's favorites.
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateFavoriteDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _service.AddFavoriteAsync(userId, dto.TvShowId);
            return NoContent();
        }

        //Remove a TV show from user's favorites.
        [HttpDelete("{tvShowId:int}")]
        public async Task<IActionResult> Remove(int tvShowId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _service.RemoveFavoriteAsync(userId, tvShowId);
            return NoContent();
        }

        //Get all favorite TV shows of the logged-in user.
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var items = await _service.GetFavoritesAsync(userId);
            return Ok(items);
        }
    }
}
