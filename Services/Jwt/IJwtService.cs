namespace expenses_api.Services.Jwt;

public interface IJwtService
{
    public Guid? GetUserId();
    string GenerateJwtToken(string username, Guid userId);
}