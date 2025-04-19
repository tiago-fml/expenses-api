using expenses_api.Data;
using expenses_api.Models;
using expenses_api.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace expenses_api.Repositories.Users;

public class CategoryRepository(ApplicationDbContext context)
    : GenericRepository<Category>(context), ICategoryRepository
{
    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x=>x.Id==id);
        return category;
    }

    public async Task<IEnumerable<Category>> GetAllUserCategoriesAsync(Guid userId)
    {
        var list = await _context.Categories.Where(x=>x.UserId == userId)
            .ToListAsync();
        return list;
    }

    public async Task<IEnumerable<Category>> GetAllDefaultCategoriesAsync()
    {
        var list = await _context.Categories.Where(x=>x.IsDefault == true)
            .ToListAsync();
        return list;
    }
}