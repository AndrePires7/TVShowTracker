using Microsoft.AspNetCore.Mvc;
using TVShowTracker.API.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace TVShowTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        //Injected authentication service
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }


        //Endpoint to register a new user
        [HttpPost("register")]
        [SwaggerRequestExample(typeof(UserRegisterDto), typeof(UserRegisterDtoExample))]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            try
            {
                var result = await _authService.Register(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        //Endpoint to login user and return JWT
        [HttpPost("login")]
        [SwaggerRequestExample(typeof(UserLoginDto), typeof(UserLoginDtoExample))]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            try
            {
                var result = await _authService.Login(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
