using AutoMapper;
using expenses_api.DTOs.Role;
using expenses_api.DTOs.User;
using expenses_api.Enums;
using expenses_api.Models;
using expenses_api.Repositories.Users;
using expenses_api.Utils;

namespace expenses_api.Services;

public class UserService(IUserRepository userRepo, IMapper mapper) : IUserService
{
    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var user = await userRepo.GetUserByIdAsync(id);
        return mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var userList = await userRepo.GetAllUsersAsync();
        return mapper.Map<List<UserDto>>(userList);
    }

    public async Task<UserDto> AddUserAsync(UserCreateDto userCreateDto, Roles role)
    {
        await VerifyUserDetails(userCreateDto.Username, userCreateDto.Email, userCreateDto.Password);
        
        var user = new User();
        
        mapper.Map(userCreateDto, user);
        user.Role = role;
        user.HashedPassword = PasswordHasher.HashPassword(userCreateDto.Password);

        userRepo.AddUser(user);
        
        await userRepo.SaveChangesAsync();
        
        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto)
    {
        var user = await userRepo.GetUserByIdAsync(id);

        if (user == null)
        {
            return null;
        }

        mapper.Map(userUpdateDto, user);

        await userRepo.SaveChangesAsync();

        return mapper.Map<UserDto>(user);
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await userRepo.GetUserByIdAsync(id);

        if (user == null) return false;
        
        userRepo.DeleteUser(user);
        
        await userRepo.SaveChangesAsync();
        
        return true;
    }

    public List<RoleDto> GetAllUserRoles()
    {
        List<RoleDto> roleList = Enum.GetValues(typeof(Roles))
            .Cast<Roles>()
            .Select(x=> new RoleDto 
            {
                Id = (int)x,
                Description = x.ToString()
            } )
            .ToList();

        return roleList;
    }

    private async Task VerifyUserDetails(string userName, string email, string password)
    {
        var user = await userRepo.GetUserByUserNameAsync(userName);

        if (user is not null)
        {
            throw new Exception($"The username {userName} is already in use.");
        }
        
        user = await userRepo.GetUserByEmailAsync(email);
        
        if (user is not null)
        {
            throw new Exception($"The email {email} is already in use.");
        }

        if (password.Length < 8 || !password.Any(char.IsDigit) || 
            !password.Any(char.IsLetter) || !password.Any(char.IsUpper))
        {
            throw new Exception(@"Password must have at least 8 characters, including at 
                least one number and one letter in upper case");
        }
    }
}