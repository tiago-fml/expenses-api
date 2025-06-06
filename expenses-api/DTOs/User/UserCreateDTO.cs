namespace expenses_api.DTOs.User;

public class UserCreateDTO
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}