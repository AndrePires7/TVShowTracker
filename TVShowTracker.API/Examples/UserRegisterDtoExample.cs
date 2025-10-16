using Swashbuckle.AspNetCore.Filters;
using TVShowTracker.API.DTO;

//This class provides a sample/example object for the UserRegisterDto.
public class UserRegisterDtoExample : IExamplesProvider<UserRegisterDto>
{
    public UserRegisterDto GetExamples()
    {
        return new UserRegisterDto
        {
            Name = "Name",
            Email = "email@gmail.com",
            Password = "Password123!"
        };
    }
}
