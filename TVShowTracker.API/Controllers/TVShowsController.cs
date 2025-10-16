using Microsoft.AspNetCore.Mvc;
using TVShowTracker.API.Services;

namespace TVShowTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TVShowsController : ControllerBase
    {
        private readonly ITVShowService _service;

        public TVShowsController(ITVShowService service)
        {
            _service = service;
        }

        //List all TV shows with optional filters, sort and pagination.
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? genre,
            [FromQuery] string? type,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetPagedAsync(genre, type, search, sortBy, page, pageSize);
            return Ok(result);
        }

        //Get details of a TV show by ID, including episodes and featured actors.
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
                return NotFound();

            return Ok(dto);
        }


        //Returns cached list of distinct genres
        [HttpGet("genres")]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _service.GetGenresAsync();
            return Ok(genres);
        }

        //Returns cached list of distinct types
        [HttpGet("types")]
        public async Task<IActionResult> GetTypes()
        {
            var types = await _service.GetTypesAsync();
            return Ok(types);
        }

    }
}
