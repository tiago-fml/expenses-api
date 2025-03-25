using expenses_api.Models;

namespace expenses_api.Repositories.Users;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
    Task<List<User>> GetAllUsersAsync();
    void AddUser(User user);
    void DeleteUser(User user); 
}