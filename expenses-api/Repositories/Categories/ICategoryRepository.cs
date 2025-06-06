using expenses_api.Models;
using expenses_api.Repositories.GenericRepository;

namespace expenses_api.Repositories.Users;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category?> GetCategoryByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetAllUserCategoriesAsync(Guid userId);
    Task<IEnumerable<Category>> GetAllDefaultCategoriesAsync();
}