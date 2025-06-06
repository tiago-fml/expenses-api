namespace expenses_api.DTOs.User;

public class UserAuthDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string HashedPassword { get; set; }
}