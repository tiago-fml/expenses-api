namespace expenses_api.Services.Jwt;

public interface IJwtService
{
    string GenerateJwtToken(string username, Guid userId);
}