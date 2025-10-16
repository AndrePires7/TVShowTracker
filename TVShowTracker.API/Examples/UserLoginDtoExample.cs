using Swashbuckle.AspNetCore.Filters;
using TVShowTracker.API.DTO;

// This class provides example data for the UserLoginDto class.
public class UserLoginDtoExample : IExamplesProvider<UserLoginDto>
{
    public UserLoginDto GetExamples()
    {
        return new UserLoginDto
        {
            Email = "email@gmail.com",
            Password = "Password123!"
        };
    }
}
