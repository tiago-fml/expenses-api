using expenses_api.DTOs.Role;
using expenses_api.DTOs.User;
using expenses_api.Enums;
using expenses_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace expenses_api.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        try
        {
            return Ok(await userService.GetAllUsersAsync());
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(GetAllUsers)}" + e.Message);
        }
    }
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult> GetUserById(Guid id)
    {
        try
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(GetUserById)}" + e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser(UserCreateDto createDto)
    {
        try
        {
            return Ok(await userService.AddUserAsync(createDto, Roles.User));
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(AddUser)}" + e.Message);
        }
    }
    
    [HttpPost("create-admin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UserDto>> AddAdmin(UserCreateDto createDto)
    {
        try
        {
            return Ok(await userService.AddUserAsync(createDto, Roles.Admin));
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(AddAdmin)}" + e.Message);
        }
    }
    
    [HttpPost("create-employee")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UserDto>> AddEmployee(UserCreateDto createDto)
    {
        try
        {
            return Ok(await userService.AddUserAsync(createDto, Roles.Employee));
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(AddEmployee)}" + e.Message);
        }
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserDto>> UpdateUser(Guid id, UserUpdateDto userUpdateDto)
    {
        try
        {
            var updatedUser = await userService.UpdateUserAsync(id, userUpdateDto);
            if (updatedUser != null)
            {
                return Ok(updatedUser);
            }

            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(UpdateUser)}" + e.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]    
    public async Task<ActionResult> DeleteUserById(Guid id)
    {
        try
        {
            var deletedUser = await userService.DeleteUserAsync(id);
            return Ok($"User with id: {id} was deleted successfully!");

            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(DeleteUserById)}" + e.Message);
        }
    }
    
    [HttpGet("roles")]
    public ActionResult<List<RoleDto>> GetAllUserRoles()
    {
        try
        {
            return Ok(userService.GetAllUserRoles());
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(GetAllUsers)}" + e.Message);
        }
    }
}