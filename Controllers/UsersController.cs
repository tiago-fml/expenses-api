using expenses_api.DTOs.User;
using expenses_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace expenses_api.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController(IUserService userService) : ControllerBase
{
    /*
    [HttpGet]
    //[Authorize(Roles = "Admin")]
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
    */
    
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
    public async Task<ActionResult<UserDTO>> AddUser(UserCreateDTO createDto)
    {
        try
        {
            return Ok(await userService.AddUserAsync(createDto));
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(AddUser)}" + e.Message);
        }
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserDTO>> UpdateUser(Guid id, UserUpdateDTO userUpdateDto)
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
    //[Authorize(Roles = "Admin")]    
    public async Task<ActionResult> DeleteUserById(Guid id)
    {
        try
        {
            await userService.DeleteUserAsync(id);
            return Ok($"User with id: {id} was deleted successfully!");
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(DeleteUserById)}" + e.Message);
        }
    }
}