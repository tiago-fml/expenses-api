using expenses_api.Data;
using expenses_api.Models;
using Microsoft.EntityFrameworkCore;

namespace expenses_api.Repositories.Users;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetUserByUserNameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Username == username);
    }   

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await context.Users.ToListAsync();
    }

    public void AddUser(User user)
    {
        context.Users.Add(user);
    }
    
    public void DeleteUser(User user)
    {
        context.Users.Remove(user);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}