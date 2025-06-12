using AutoMapper;
using expenses_api.Data;
using expenses_api.DTOs.User;
using expenses_api.Repositories.UnityOfWork;
using expenses_api.Services.Jwt;
using expenses_api.Services.Users;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace expenses_api.IntegrationTests;

public class UserServiceIntegrationTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer;
    private ApplicationDbContext _dbContext = default!;
    private IUserService _userService = default!;

    public UserServiceIntegrationTests()
    {
        _postgresContainer = new PostgreSqlBuilder()
            .WithImage("postgres:16")
            .WithDatabase("testdb")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithCleanUp(true)
            .Build();
    }
    
    public async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_postgresContainer.GetConnectionString())
            .Options;

        _dbContext = new ApplicationDbContext(options);

        // Apply migrations or ensure DB created
        await _dbContext.Database.MigrateAsync();
        
        IUnitOfWork unitOfWork = new UnitOfWork(_dbContext);

        // Configure AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Utils.Mapper>();
        });
        
        var mapper = config.CreateMapper();

        var fakeJwtService = new FakeJwtService(); // test stub for JWT

        _userService = new UserService(unitOfWork, mapper, fakeJwtService);
    }

    public async Task DisposeAsync()
    {
        await _postgresContainer.DisposeAsync();
    }
    
    [Fact]
    public async Task AddUserAsync_ShouldPersistUser()
    {
        var userDto = new UserCreateDTO
        {
            Username = "jane1",
            Password = "P_assword#1",
            FirstName = "Jane",
            LastName = "Doe"
        };

        var createdUser = await _userService.AddUserAsync(userDto);
        var users = await _userService.GetAllUsersAsync();

        var usersList = users.ToList();
        
        Assert.Single(usersList);
        Assert.Equal("Jane", usersList[0].FirstName);
    }
}

public class FakeJwtService : IJwtService
{
    public string GenerateJwtToken(string username, Guid userId) => "fake-jwt-token";
    public Guid GetUserId() => Guid.NewGuid();
}