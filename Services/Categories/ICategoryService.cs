using expenses_api.DTOs.Category;

namespace expenses_api.Services.Users;

public interface ICategoryService
{
    Task<CategoryDTO?> GetCategoryByIdAsync(Guid id);
    Task<IEnumerable<CategoryDTO>> GetAllDefaultCategoriesAsync();
    Task<IEnumerable<CategoryDTO>> GetAllUserCategoriesAsync();
    Task<CategoryDTO> AddCategoryAsync(CategoryCreateDTO categoryCreateDto);
    Task<CategoryDTO?> UpdateCategoryAsync(Guid id, CategoryUpdateDTO categoryUpdateDto);
    Task DeleteCategoryAsync(Guid id);
}