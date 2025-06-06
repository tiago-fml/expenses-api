using expenses_api.DTOs.Category;
using expenses_api.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace expenses_api.Controllers;

[Authorize]
[ApiController]
[Route("api/categories")]
public class CategoriesController(ICategoryService _categoryService) : ControllerBase
{
    [HttpGet("default")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetDefaultCategories()
    {
        try
        {
            var list = await _categoryService.GetAllDefaultCategoriesAsync();
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(GetDefaultCategories)}" + e.Message);
        }
    }
    
    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetUserCategories()
    {
        try
        {
            var list = await _categoryService.GetAllUserCategoriesAsync();
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(GetUserCategories)}" + e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDTO>> GetCategory(Guid id)
    {
        try
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(GetCategory)}" + e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> AddCategory(CategoryCreateDTO categoryCreateDto)
    {
        try
        {
            var category = await _categoryService.AddCategoryAsync(categoryCreateDto);
            return Ok(category);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(AddCategory)}: " + e.Message);
        }
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDTO>> UpdateCategory(Guid id, CategoryUpdateDTO categoryUpdateDto)
    {
        try
        {
            var category = await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto);
            return Ok(category);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(UpdateCategory)}" + e.Message);
        }
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(DeleteCategory)}" + e.Message);
        }
    }
}