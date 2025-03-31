using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace expenses_api.Services.Jwt;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userId, out var guid) ? guid : null;
    }
    
    public string GenerateJwtToken(string username, Guid userId)
    {
        var jwtKey = _configuration["Jwt:Key"];
        if (jwtKey is null)
        {
            throw new Exception("JWT Key is missing from AppSettings");
        }
        
        var key = Encoding.ASCII.GetBytes(jwtKey);

        int.TryParse(_configuration["Jwt:TokenDurationInMinutes2"], out var tokenDurationInMinutes);
        var durationInMinutes = tokenDurationInMinutes == 0 ? 360 : tokenDurationInMinutes;

        var tokenHandler = new JwtSecurityTokenHandler();
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            ]),
            Expires = DateTime.UtcNow.AddMinutes(durationInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}