using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TVShowTracker.API.Services;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RecommendationsController : ControllerBase
{
    private readonly IRecommendationService _service;

    //Constructor injection of the RecommendationService
    public RecommendationsController(IRecommendationService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        //Extract the current user's ID from the JWT token claims
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

        //Call the service to get recommended TV shows for this user
        var recommendations = await _service.GetRecommendationsAsync(userId);

        return Ok(recommendations);
    }
}
