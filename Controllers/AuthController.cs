using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using expenses_api.DTOs.User;
using expenses_api.Models;
using expenses_api.Repositories.Users;
using expenses_api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace expenses_api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IConfiguration configuration, IUserRepository userRepository)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLogin model)
    {
        var user = await userRepository.GetUserByUserNameAsync(model.Username);
        if (user is null)
        {
            return NotFound("User not found");
        }
        
        // Replace this with your own user authentication logic
        if (!PasswordHasher.VerifyPassword(user.HashedPassword, model.Password)) return Unauthorized();
        
        var tokenString = GenerateJwtToken(user);

        return Ok(new { Token = tokenString });
    }
    
    private string GenerateJwtToken(User user)
    {
        var jwtKey = configuration["Jwt:Key"];
        if (jwtKey is null)
        {
            throw new Exception("JWT Key is missing from AppSettings");
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}

