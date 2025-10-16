using BCrypt.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TVShowTracker.API.Data;
using TVShowTracker.API.DTO;
using TVShowTracker.API.Models;

//Service responsible for handling user authentication and registration
public class AuthService
{

    //Database context for accessing users
    private readonly AppDbContext _context;

    //App configuration to read JWT secret key
    private readonly IConfiguration _config; 

    public AuthService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }


    //Registers a new user
    public async Task<UserResponseDto> Register(UserRegisterDto dto)
    {
        //Check if email is already registered
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            throw new Exception("Email already registered");

        //Create new user object with hashed password
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        //Add user to the database
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        //Generate JWT token for the new user
        var token = GenerateJwtToken(user);

        //Return DTO containing user info and token
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Token = token
        };
    }

    //Logs in a user and returns JWT
    public async Task<UserResponseDto> Login(UserLoginDto dto)
    {

        //Find user by email
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        //Check if user exists and password matches
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid email or password");

        //Generate JWT token
        var token = GenerateJwtToken(user);


        //Return DTO containing user info and token
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Token = token
        };
    }

    //Generates JWT token for a given user
    private string GenerateJwtToken(User user)
    {
        //Create an instance of JwtSecurityTokenHandler
        var tokenHandler = new JwtSecurityTokenHandler();

        //Read secret key from config
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

        //Define claims to include in the token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            }),

            // Token valid for 2 hours
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(

                //Use secret key to sign token
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        //Generate a new JWT token using the previously defined tokenDescriptor,
        var token = tokenHandler.CreateToken(tokenDescriptor);

        //Return serialized JWT token
        return tokenHandler.WriteToken(token);
    }
}
