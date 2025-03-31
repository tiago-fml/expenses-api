using expenses_api.Models;
using expenses_api.Repositories.GenericRepository;

namespace expenses_api.Repositories.Users;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
    Task<List<User>> GetAllUsersAsync();
}