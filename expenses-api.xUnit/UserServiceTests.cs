using AutoMapper;
using expenses_api.DTOs.User;
using expenses_api.Models;
using expenses_api.Repositories.UnityOfWork;
using expenses_api.Services.Jwt;
using expenses_api.Services.Users;
using Moq;

namespace expenses_api.xUnit;

public class UserServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly UserService _userService;
    
    public UserServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _jwtServiceMock = new Mock<IJwtService>();

        // Configure AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Utils.Mapper>();
        });
        
        var mapper = config.CreateMapper();

        _userService = new UserService(_unitOfWorkMock.Object, mapper, _jwtServiceMock.Object);
    }
    
    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId, 
            Username = "username", 
            Email = "test@email.com",
            FirstName = "UserFirstName", 
            LastName = "UserLastName", 
            BirthDate = new DateTime(2000, 1, 1),
        };

        _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByIdAsync(userId))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserByIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
        Assert.Equal(user.Username, result.Username);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.FirstName, result.FirstName);
        Assert.Equal(user.LastName, result.LastName);
        Assert.Equal(user.BirthDate, result.BirthDate);
    }
    
    [Fact]
    public async Task GetUserByUsernameAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId, 
            Username = "username", 
            Email = "test@email.com",
            FirstName = "UserFirstName", 
            LastName = "UserLastName", 
            BirthDate = new DateTime(2000, 1, 1),
        };

        _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByUsernameAsync("username"))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserByUsernameAsync("username");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
        Assert.Equal(user.Username, result.Username);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.FirstName, result.FirstName);
        Assert.Equal(user.LastName, result.LastName);
        Assert.Equal(user.BirthDate, result.BirthDate);
    }
    
    [Fact]
    public async Task GetUserByUsernameAsync_ShouldReturnNull_WhenUserNotFound()
    {
        _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByUsernameAsync("unknown"))
            .ReturnsAsync((User?)null);

        var result = await _userService.GetUserByUsernameAsync("unknown");

        Assert.Null(result);
    }
    
    [Fact]
    public async Task AddUserAsync_ShouldAddUser_WhenValid()
    {
        var userDto = new UserCreateDTO
        {
            Username = "jane1",
            Password = "P_assword#1",
            FirstName = "Jane",
            LastName = "Doe"
        };

        _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByUsernameAsync(userDto.Username))
            .ReturnsAsync((User?)null);

        _unitOfWorkMock.Setup(u => u.UserRepository.Add(It.IsAny<User>()));
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

        var result = await _userService.AddUserAsync(userDto);

        Assert.NotNull(result);
        Assert.Equal("jane1", result.Username);
        Assert.Equal("Jane", result.FirstName);
        Assert.Equal("Doe", result.LastName);
    }
    
    [Fact]
    public async Task AddUserAsync_ShouldThrowException_WhenUsernameExists()
    {
        var existingUser = new User { Username = "jane" };

        _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByUsernameAsync("jane"))
            .ReturnsAsync(existingUser);

        var dto = new UserCreateDTO
        {
            Username = "jane",
            Password = "Password1"
        };

        await Assert.ThrowsAsync<Exception>(() => _userService.AddUserAsync(dto));
    }

    [Fact]
    public async Task UpdateUserAsync_ShouldUpdate_WhenUserExists()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, Username = "test", BirthDate = new DateTime(2000, 1, 1)};

        _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByIdAsync(userId))
            .ReturnsAsync(user);

        _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

        var updateDto = new UserUpdateDTO { FirstName = "Updated", LastName = "Updated",
            BirthDate = new DateTime(2001, 1, 1) };

        var result = await _userService.UpdateUserAsync(userId, updateDto);

        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
        Assert.Equal(updateDto.FirstName, result.FirstName);
        Assert.Equal(updateDto.LastName, result.LastName);
        Assert.Equal(updateDto.BirthDate, result.BirthDate);
    }
    
    [Fact]
    public async Task DeleteUserAsync_ShouldDelete_WhenUserExists()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, Username = "test"};

        _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByIdAsync(userId))
            .ReturnsAsync(user);

        _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
        
        await _userService.DeleteUserAsync(userId);

        _unitOfWorkMock.Verify(u => u.UserRepository.GetUserByIdAsync(userId), Times.Once);
        _unitOfWorkMock.Verify(u => u.UserRepository.Remove(user), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
    
    [Fact]
    public async Task DeleteUserAsync_ShouldThrowException_WhenUserDoesNotExist()
    {
        var userId = Guid.NewGuid();

        _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByIdAsync(userId))
            .ReturnsAsync((User?)null);  // Return null to simulate user not found

        var exception = await Assert.ThrowsAsync<Exception>(() => _userService.DeleteUserAsync(userId));

        Assert.Equal($"The user with id: {userId} was not found.", exception.Message);

        _unitOfWorkMock.Verify(u => u.UserRepository.GetUserByIdAsync(userId), Times.Once);
        _unitOfWorkMock.Verify(u => u.UserRepository.Remove(It.IsAny<User>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
    }
    
    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnAllUsers()
    {
        // Arrange
        List<User> users = [
            new() { Id = Guid.NewGuid(), Username = "test", FirstName = "User1" },
            new() { Id = Guid.NewGuid(), Username = "test2", FirstName = "User2"},
            new() { Id = Guid.NewGuid(), Username = "test3", FirstName = "User3"}
        ];

        _unitOfWorkMock.Setup(u => u.UserRepository.GetAllUsersAsync())
            .ReturnsAsync(users);

        // Act
        var result = await _userService.GetAllUsersAsync();
        
        // Assert
        Assert.NotNull(result); 
        
        var resultList = result.ToList();
        
        Assert.Equal(3, resultList.Count);
        Assert.Equal(users[0].Id, resultList[0].Id);
        Assert.Equal(users[1].Username, resultList[1].Username);
        Assert.Equal(users[2].FirstName, resultList[2].FirstName);
        
        _unitOfWorkMock.Verify(u => u.UserRepository.GetAllUsersAsync(), Times.Once);
    }
    
    [Fact]
    public async Task GetUserForAuthenticationAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(), 
            Username = "username", 
            Email = "test@email.com",
            FirstName = "UserFirstName", 
            LastName = "UserLastName", 
            BirthDate = new DateTime(2000, 1, 1),
        };

        _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByUsernameAsync("username"))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserForAuthenticationAsync("username");

        // Assert
        Assert.NotNull(result);
        
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Username, result.Username);
    }
    
    [Fact]
    public async Task GetCurrentUserAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(), 
            Username = "username", 
            Email = "test@email.com",
            FirstName = "UserFirstName", 
            LastName = "UserLastName", 
            BirthDate = new DateTime(2000, 1, 1),
        };

        _jwtServiceMock.Setup(u => u.GetUserId())
            .Returns(user.Id);
        
        _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByIdAsync(user.Id))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.GetCurrentUserAsync();

        // Assert
        Assert.NotNull(result);
        
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Username, result.Username);
    }
}