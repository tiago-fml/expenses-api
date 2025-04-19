using AutoMapper;
using expenses_api.DTOs.Category;
using expenses_api.Models;
using expenses_api.Repositories.UnityOfWork;
using expenses_api.Services.Jwt;

namespace expenses_api.Services.Users;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    
    public CategoryService(IMapper mapper, IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }
    
    public async Task<CategoryDTO?> GetCategoryByIdAsync(Guid id)
    {
        var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);
        return _mapper.Map<CategoryDTO>(category);
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllDefaultCategoriesAsync()
    {
        var list = await _unitOfWork.CategoryRepository.GetAllDefaultCategoriesAsync();
        return _mapper.Map<IEnumerable<CategoryDTO>>(list);
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllUserCategoriesAsync()
    {
        var userId = _jwtService.GetUserId();
        var list = await _unitOfWork.CategoryRepository.GetAllUserCategoriesAsync(userId);
        return _mapper.Map<IEnumerable<CategoryDTO>>(list);
    }

    public async Task<CategoryDTO> AddCategoryAsync(CategoryCreateDTO categoryCreateDto)
    {
        var category = _mapper.Map<Category>(categoryCreateDto);
        category.UserId = _jwtService.GetUserId();
        
        _unitOfWork.CategoryRepository.Add(category);
        
        await _unitOfWork.SaveChangesAsync();
        
        return _mapper.Map<CategoryDTO>(category);
    }

    public async Task<CategoryDTO?> UpdateCategoryAsync(Guid id, CategoryUpdateDTO categoryUpdateDto)
    {
        var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);

        if (category.IsDefault)
        {
            throw new Exception("Cannot update default category");
        }
        
        _unitOfWork.CategoryRepository.Add(category);
        
        await _unitOfWork.SaveChangesAsync();
        
        return _mapper.Map<CategoryDTO>(category);
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);

        if (category.IsDefault)
        {
            throw new Exception("Cannot update default category");
        }

        if (category.UserId != _jwtService.GetUserId())
        {
            throw new Exception("Not allowed to delete this category");
        }
        
        _unitOfWork.CategoryRepository.Remove(category);
        
        await _unitOfWork.SaveChangesAsync();
    }
}