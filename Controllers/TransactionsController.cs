using expenses_api.DTOs.Transaction;
using expenses_api.Services.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace expenses_api.Controllers;

[Authorize]
[ApiController]
[Route("api/transactions")]
public class TransactionsController(ITransactionService _transactionsService) : ControllerBase
{
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetUserTransactions(Guid userId, DateTime startDate, DateTime endDate)
    {
        try
        {
            var list = await _transactionsService.GetTransactionsByUserIdAsync(userId,startDate, endDate);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(GetUserTransactions)}" + e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionDto>> GetTransaction(Guid id)
    {
        try
        {
            var transaction = await _transactionsService.GetTransactionAsync(id);
            return Ok(transaction);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(GetTransaction)}" + e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAllTransactions()
    {
        try
        {
            var list = await _transactionsService.GetAllTransactionsAsync();
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(GetAllTransactions)}" + e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<TransactionDto>> AddTransaction(TransactionCreateDto transactionCreateDto)
    {
        try
        {
            var transaction = await _transactionsService.AddTransactionAsync(transactionCreateDto);
            return Ok(transaction);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(AddTransaction)}" + e.Message);
        }
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<TransactionDto>> UpdateTransaction(Guid id, TransactionUpdateDto transactionUpdateDto)
    {
        try
        {
            var transaction = await _transactionsService.UpdateTransactionAsync(id, transactionUpdateDto);
            return Ok(transaction);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(UpdateTransaction)}" + e.Message);
        }
    }
}