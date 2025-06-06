using AutoMapper;
using expenses_api.DTOs.User;
using expenses_api.Models;
using expenses_api.Repositories.UnityOfWork;
using expenses_api.Services.Jwt;
using expenses_api.Utils;

namespace expenses_api.Services.Users;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper, IJwtService _jwtService) : IUserService
{
    public async Task<UserDTO> GetCurrentUserAsync()
    {
        var userId = _jwtService.GetUserId();
        var user = await unitOfWork.UserRepository.GetUserByIdAsync(userId);
        return mapper.Map<UserDTO>(user);
    }
    
    public async Task<UserDTO?> GetUserByIdAsync(Guid id)
    {
        var user = await unitOfWork.UserRepository.GetUserByIdAsync(id);
        return mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO?> GetUserByUsernameAsync(string username)
    {
        var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(username);
        return user == null ? null : mapper.Map<UserDTO>(user);
    }

    public async Task<UserAuthDTO?> GetUserForAuthenticationAsync(string username)
    {
        var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(username);
        return user == null ? null : mapper.Map<UserAuthDTO>(user);
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        var userList = await unitOfWork.UserRepository.GetAllUsersAsync();
        return mapper.Map<List<UserDTO>>(userList);
    }

    public async Task<UserDTO> AddUserAsync(UserCreateDTO userCreateDto)
    {
        //check if there's any user with the username
        var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(userCreateDto.Username);
        if (user is not null)
        {
            throw new Exception($"The username {userCreateDto.Username} is already in use.");
        }
        
        if (!Helpers.IsPasswordValid(userCreateDto.Password))
        {
            throw new Exception(@"Password must have at least 8 characters, including at 
                least one number and one letter in upper case");
        }
        
        user = new User();
        mapper.Map(userCreateDto, user);
        
        user.HashedPassword = PasswordHasher.HashPassword(userCreateDto.Password);

        unitOfWork.UserRepository.Add(user);
        
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO?> UpdateUserAsync(Guid id, UserUpdateDTO userUpdateDto)
    {
        var user = await unitOfWork.UserRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            return null;
        }

        mapper.Map(userUpdateDto, user);

        await unitOfWork.SaveChangesAsync();

        return mapper.Map<UserDTO>(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await unitOfWork.UserRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new Exception($"The user with id: {id} was not found.");
        }
        
        unitOfWork.UserRepository.Remove(user);
        
        await unitOfWork.SaveChangesAsync();
    }
}