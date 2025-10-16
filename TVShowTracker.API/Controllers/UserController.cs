using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TVShowTracker.API.Repositories;
using TVShowTracker.API.Services;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepo;
    private readonly IExportService _exportService;

    public UserController(IUserRepository userRepo, IExportService exportService)
    {
        _userRepo = userRepo;
        _exportService = exportService;
    }


    //Endpoint to export the current user's information and favorite shows as CSV
    [HttpGet("me/export/csv")]
    public async Task<IActionResult> ExportFavoritesCsv()
    {
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var user = await _userRepo.GetByIdAsync(userId);

        if (user == null)
            return NotFound("User not found.");

        var bytes = await _exportService.ExportUserFavoritesToCsvAsync(user);
        return File(bytes, "text/csv", $"user_{userId}_favorites.csv");
    }


    //Endpoint to export the current user's information and favorite shows as PDF
    [HttpGet("me/export/pdf")]
    public async Task<IActionResult> ExportFavoritesPdf()
    {
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var user = await _userRepo.GetByIdAsync(userId);

        if (user == null)
            return NotFound("User not found.");

        var bytes = await _exportService.ExportUserFavoritesToPdfAsync(user);
        return File(bytes, "application/pdf", $"user_{userId}_favorites.pdf");
    }


    //Endpoint to delete the current user's account (RGPD compliance)
    [HttpDelete("me")]
    public async Task<IActionResult> DeleteMyAccount()
    {
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var user = await _userRepo.GetByIdAsync(userId);

        if (user == null)
            return NotFound("User not found.");

        user.FavoriteShows.Clear();
        await _userRepo.DeleteAsync(user);

        return NoContent();
    }
}
