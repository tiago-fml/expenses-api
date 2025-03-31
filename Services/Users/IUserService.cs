using expenses_api.DTOs.User;

namespace expenses_api.Services.Users;

public interface IUserService
{
    Task<UserDTO?> GetUserByIdAsync(Guid userId);
    Task<UserDTO?> GetUserByUsernameAsync(string username);
    Task<UserAuthDTO> GetUserForAuthenticationAsync(string username);
    Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    Task<UserDTO> AddUserAsync(UserCreateDTO userCreateDto);
    Task<UserDTO?> UpdateUserAsync(Guid id, UserUpdateDTO userUpdateDto);
    Task DeleteUserAsync(Guid userId);
}