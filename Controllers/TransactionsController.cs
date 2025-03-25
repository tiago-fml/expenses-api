using System.Security.Claims;
using expenses_api.Services.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace expenses_api.Controllers;

[Authorize]
[ApiController]
[Route("api/transactions")]
public class TransactionsController(ITransactionService transactionsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> TestTransactionService()
    {
        try
        {
            // Extract the userId from the JWT token claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var list = await transactionsService.GetAllTransactionsAsync();
            
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(TestTransactionService)}" + e.Message);
        }
    }
}