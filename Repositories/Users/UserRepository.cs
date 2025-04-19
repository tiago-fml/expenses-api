using expenses_api.Data;
using expenses_api.Models;
using expenses_api.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace expenses_api.Repositories.Users;

public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository
{
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
    }   

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
}