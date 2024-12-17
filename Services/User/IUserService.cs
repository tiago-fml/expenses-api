using expenses_api.DTOs.Role;
using expenses_api.DTOs.User;
using expenses_api.Enums;

namespace expenses_api.Services;

public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(Guid userId);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> AddUserAsync(UserCreateDto user, Roles role);
    Task<UserDto?> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto);
    Task<bool> DeleteUserAsync(Guid userId);
    List<RoleDto> GetAllUserRoles();
}