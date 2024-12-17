using expenses_api.DTOs.Role;
using expenses_api.DTOs.Transaction;
using expenses_api.DTOs.User;
using expenses_api.Enums;
using expenses_api.Services.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace expenses_api.Controllers;

[ApiController]
[Route("api/transactions")]
//[Authorize]
public class TransactionsController(ITransactionService transactionsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> TestTransactionService()
    {
        try

        {
            var transactionCreateDto = new TransactionCreateDto
            {
                Description = "teste",
                CategoryId = Guid.Parse("f44b906c-d079-42f1-8e19-e0fdc3c18061"),
                ExecutedAt = DateTime.Now,
                Type = TransactionType.INCOME,
                UserId = Guid.Parse("e2a6fdaa-5fb5-4041-91a6-2077d0f6a4c7"),
                Value = 10,
                Id = Guid.NewGuid()
            };
            
            var transaction = await transactionsService.AddTransactionAsync(transactionCreateDto);

            var list = await transactionsService.GetAllTransactionsAsync();
            
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(TestTransactionService)}" + e.Message);
        }
    }
    
    /*[HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        try
        {
            return Ok(await transactionsService.GetAllUsersAsync());
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(GetAllUsers)}" + e.Message);
        }
    }*/
    
    /*[HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult> GetUserById(Guid id)
    {
        try
        {
            var user = await transactionsService.GetUserByIdAsync(id);
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
    }*/
    
    /*
    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser(UserCreateDto createDto)
    {
        try
        {
            return Ok(await transactionsService.AddUserAsync(createDto, Roles.User));
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(AddUser)}" + e.Message);
        }
    }*/
    
    /*[HttpPost("create-admin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UserDto>> AddAdmin(UserCreateDto createDto)
    {
        try
        {
            return Ok(await transactionsService.AddUserAsync(createDto, Roles.Admin));
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
            return Ok(await transactionsService.AddUserAsync(createDto, Roles.Employee));
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
            var updatedUser = await transactionsService.UpdateUserAsync(id, userUpdateDto);
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
            var deletedUser = await transactionsService.DeleteUserAsync(id);
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
            return Ok(transactionsService.GetAllUserRoles());
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(GetAllUsers)}" + e.Message);
        }
    }*/
}